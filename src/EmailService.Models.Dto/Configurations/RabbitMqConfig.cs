using System.Collections.Generic;
using UniversityHelper.Core.BrokerSupport.Attributes;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Models.Broker.Requests.Company;
using UniversityHelper.Models.Broker.Requests.Email;

namespace UniversityHelper.EmailService.Models.Dto.Configurations;

public class RabbitMqConfig : BaseRabbitMqConfig
{
  public string SendEmailEndpoint { get; set; }
  public string CreateSmtpCredentialsEndpoint { get; set; }
  public Dictionary<string, string> FindUserParseEntitiesEndpoint { get; set; }

  [AutoInjectRequest(typeof(ICreateSmtpCredentialsRequest))]
  public string GetSmtpCredentialsEndpoint { get; set; }
}
