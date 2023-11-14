using UniversityHelper.EmailService.Mappers.Models.Interfaces;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.EmailService.Models.Dto.Models;

namespace UniversityHelper.EmailService.Mappers.Models;

public class EmailInfoMapper : IEmailInfoMapper
{
  public EmailInfo Map(DbEmail dbEmail)
  {
    if (dbEmail == null)
    {
      return null;
    }

    return new EmailInfo
    {
      Id = dbEmail.Id,
      Body = dbEmail.Text,
      Subject = dbEmail.Subject,
      Receiver = dbEmail.Receiver
    };
  }
}
