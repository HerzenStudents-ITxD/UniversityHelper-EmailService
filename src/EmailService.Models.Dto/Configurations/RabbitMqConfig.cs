using System.Collections.Generic;
using HerzenHelper.Core.BrokerSupport.Attributes;
using HerzenHelper.Core.BrokerSupport.Configurations;
using HerzenHelper.Models.Broker.Requests.Company;
using HerzenHelper.Models.Broker.Requests.Email;

namespace HerzenHelper.EmailService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    public string SendEmailEndpoint { get; set; }
    public string CreateSmtpCredentialsEndpoint { get; set; }
    public Dictionary<string, string> FindUserParseEntitiesEndpoint { get; set; }

    [AutoInjectRequest(typeof(ICreateSmtpCredentialsRequest))]
    public string GetSmtpCredentialsEndpoint { get; set; }
  }
}
