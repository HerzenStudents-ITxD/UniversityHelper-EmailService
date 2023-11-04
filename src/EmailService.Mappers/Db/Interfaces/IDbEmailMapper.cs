using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Requests.Email;

namespace HerzenHelper.EmailService.Mappers.Db.Email.Interfaces
{
  [AutoInject]
  public interface IDbEmailMapper
  {
    DbEmail Map(ISendEmailRequest request);
  }
}
