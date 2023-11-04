using HerzenHelper.EmailService.Mappers.Models.Interfaces;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.EmailService.Models.Dto.Models;

namespace HerzenHelper.EmailService.Mappers.Models
{
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
}
