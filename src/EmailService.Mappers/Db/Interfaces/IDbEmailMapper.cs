using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Requests.Email;

namespace UniversityHelper.EmailService.Mappers.Db.Email.Interfaces
{
  [AutoInject]
  public interface IDbEmailMapper
  {
    DbEmail Map(ISendEmailRequest request);
  }
}
