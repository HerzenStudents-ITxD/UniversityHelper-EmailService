using System;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Data.Interfaces;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.Models.Broker.Requests.Email;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;

namespace UniversityHelper.EmailService.Broker.Consumers;

public class CreateSmtpCredentialsConsumer : IConsumer<ICreateSmtpCredentialsRequest>
{
  private readonly ISmtpSettingsRepository _repository;
  private async Task<object> CreateCredentials(ICreateSmtpCredentialsRequest request)
  {
    DbModuleSetting dbModuleSetting = new DbModuleSetting()
    {
      Id = Guid.NewGuid(),
      Host = request.Host,
      Port = request.Port,
      EnableSsl = request.EnableSsl,
      Email = request.Email,
      Password = request.Password,
      CreatedAtUtc = DateTime.UtcNow,
    };

    bool result = await _repository.CreateAsync(dbModuleSetting);

    return result;
  }

  public CreateSmtpCredentialsConsumer(
    IMemoryCache cache,
    ISmtpSettingsRepository repository)
  {
    _repository = repository;
  }
  public async Task Consume(ConsumeContext<ICreateSmtpCredentialsRequest> context)
  {
    Object result = OperationResultWrapper.CreateResponse(CreateCredentials, context.Message);

    await context.RespondAsync<IOperationResult<bool>>(result);
  }
}
