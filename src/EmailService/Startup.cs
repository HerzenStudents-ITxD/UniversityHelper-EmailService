using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using HerzenHelper.EmailService.Broker.Consumers;
using HerzenHelper.EmailService.Broker.Helpers;
using HerzenHelper.EmailService.Broker.Helpers.ParseEntity;
using HerzenHelper.EmailService.Data.Interfaces;
using HerzenHelper.EmailService.Data.Provider.MsSql.Ef;
using HerzenHelper.EmailService.Models.Dto.Configurations;
using HerzenHelper.Core.BrokerSupport.Attributes.ParseEntity.Models.Requests;
using HerzenHelper.Core.BrokerSupport.Attributes.ParseEntity.Models.Responses;
using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Core.BrokerSupport.Configurations;
using HerzenHelper.Core.BrokerSupport.Extensions;
using HerzenHelper.Core.BrokerSupport.Middlewares.Token;
using HerzenHelper.Core.Configurations;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers;
using HerzenHelper.Core.Middlewares.ApiInformation;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HerzenHelper.EmailService
{
  public class Startup : BaseApiInfo
  {
    public const string CorsPolicyName = "LtDoCorsPolicy";

    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly BaseServiceInfoConfig _serviceInfoConfig;

    public IConfiguration Configuration { get; }

    private (string username, string password) GetRabbitMqCredentials()
    {
      static string GetString(string envVar, string formAppsettings, string generated, string fieldName)
      {
        string str = Environment.GetEnvironmentVariable(envVar);
        if (string.IsNullOrEmpty(str))
        {
          str = formAppsettings ?? generated;

          Log.Information(
            formAppsettings == null
              ? $"Default RabbitMq {fieldName} was used."
              : $"RabbitMq {fieldName} from appsetings.json was used.");
        }
        else
        {
          Log.Information($"RabbitMq {fieldName} from environment was used.");
        }

        return str;
      }

      return (GetString("RabbitMqUsername", _rabbitMqConfig.Username, $"{_serviceInfoConfig.Name}_{_serviceInfoConfig.Id}", "Username"),
        GetString("RabbitMqPassword", _rabbitMqConfig.Password, _serviceInfoConfig.Id, "Password"));
    }

    private void UpdateDatabase(IApplicationBuilder app)
    {
      using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();

      using var context = serviceScope.ServiceProvider.GetService<EmailServiceDbContext>();

      context.Database.Migrate();
    }

    private void StartResender(IApplicationBuilder app)
    {
      EmailEngineConfig emailEngineConfig = Configuration
        .GetSection(EmailEngineConfig.SectionName)
        .Get<EmailEngineConfig>();

      IServiceProvider serviceProvider = app.ApplicationServices.GetRequiredService<IServiceProvider>();

      var scope = app.ApplicationServices.CreateScope();

      IUnsentEmailRepository repository = scope.ServiceProvider.GetRequiredService<IUnsentEmailRepository>();
      ISmtpSettingsRepository getSmtpCredentials = scope.ServiceProvider.GetRequiredService<ISmtpSettingsRepository>();

      ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
      ILogger<EmailResender> logger = loggerFactory.CreateLogger<EmailResender>();
       
      var resender = new EmailResender(repository, logger, getSmtpCredentials);

      if (!int.TryParse(Environment.GetEnvironmentVariable("MaxResendingCount"), out int maxResendingCount))
      {
        maxResendingCount = emailEngineConfig.MaxResendingCount;
        Log.Information($"Max resending count from appsettings.json was used. Value '{maxResendingCount}'.");
      }
      else
      {
        Log.Information($"Max resending count from environment was used. Value '{maxResendingCount}'.");
      }

      if (!int.TryParse(Environment.GetEnvironmentVariable("ResendIntervalInMinutes"), out int resendIntervalInMinutes))
      {
        resendIntervalInMinutes = emailEngineConfig.ResendIntervalInMinutes;
        Log.Information($"Resen interval in minutes from appsettings.json was used. Value '{resendIntervalInMinutes}'.");
      }
      else
      {
        Log.Information($"Resend interval in minutes from environment was used. Value '{resendIntervalInMinutes}'.");
      }

      Task.Run(() => resender.StartResend(resendIntervalInMinutes, maxResendingCount));
    }

    private void FindParseProperties(IApplicationBuilder app)
    {
      IServiceProvider serviceProvider = app.ApplicationServices.GetRequiredService<IServiceProvider>();

      foreach (KeyValuePair<string, string> pair in _rabbitMqConfig.FindUserParseEntitiesEndpoint)
      {
        IRequestClient<IFindParseEntitiesRequest> rcFindParseEntities = serviceProvider.CreateRequestClient<IFindParseEntitiesRequest>(
          new Uri($"{_rabbitMqConfig.BaseUrl}/{pair.Value}"), default);

        try
        {
          var result = rcFindParseEntities.GetResponse<IOperationResult<IFindParseEntitiesResponse>>(IFindParseEntitiesRequest.CreateObj()).Result.Message;

          if (result.IsSuccess)
          {
            AllParseEntities.Entities.Add(pair.Key, result.Body.Entities);
          }
        }
        catch
        {

        }
      }
    }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      _serviceInfoConfig = Configuration
        .GetSection(BaseServiceInfoConfig.SectionName)
        .Get<BaseServiceInfoConfig>();

      _rabbitMqConfig = Configuration
        .GetSection(BaseRabbitMqConfig.SectionName)
        .Get<RabbitMqConfig>();

      Version = "2.0.1.0";
      Description = "EmailService, is intended to work with the emails and email templates.";
      StartTime = DateTime.UtcNow;
      ApiName = $"HerzenHelper - {_serviceInfoConfig.Name}";
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyName,
          builder =>
          {
            builder
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });

      string connStr = Environment.GetEnvironmentVariable("ConnectionString");
      if (string.IsNullOrEmpty(connStr))
      {
        connStr = Configuration.GetConnectionString("SQLConnectionString");
        Log.Information($"SQL connection string from appsettings.json was used. Value '{PasswordHider.Hide(connStr)}'.");
      }
      else
      {
        Log.Information($"SQL connection string from environment was used. Value '{PasswordHider.Hide(connStr)}'.");
      }
      services.Configure<TokenConfiguration>(Configuration.GetSection("CheckTokenMiddleware"));
      services.Configure<BaseRabbitMqConfig>(Configuration.GetSection(BaseRabbitMqConfig.SectionName));
      services.Configure<BaseServiceInfoConfig>(Configuration.GetSection(BaseServiceInfoConfig.SectionName));

      services.AddHttpContextAccessor();

      services.AddDbContext<EmailServiceDbContext>(options =>
      {
        options.UseSqlServer(connStr);
      });

      services
        .AddHealthChecks()
        .AddSqlServer(connStr)
        .AddRabbitMqCheck();

      services
        .AddControllers()
        .AddNewtonsoftJson();

      services.AddBusinessObjects();

      services.AddTransient<EmailSender>();

      services.AddMemoryCache();

      services.ConfigureMassTransit(_rabbitMqConfig);
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      UpdateDatabase(app);

      FindParseProperties(app);

      StartResender(app);

      app.UseForwardedHeaders();

      app.UseExceptionsHandler(loggerFactory);

      app.UseApiInformation();

      app.UseRouting();

      app.UseMiddleware<TokenMiddleware>();

      app.UseCors(CorsPolicyName);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers().RequireCors(CorsPolicyName);

        endpoints.MapHealthChecks($"/{_serviceInfoConfig.Id}/hc", new HealthCheckOptions
        {
          ResultStatusCodes = new Dictionary<HealthStatus, int>
          {
            { HealthStatus.Unhealthy, 200 },
            { HealthStatus.Healthy, 200 },
            { HealthStatus.Degraded, 200 },
          },
          Predicate = check => check.Name != "masstransit-bus",
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
      });
    }
  }
}
