using System;
using HerzenHelper.EmailService.Mappers.Db.Email.Interfaces;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.Models.Broker.Requests.Email;

namespace HerzenHelper.EmailService.Mappers.Db.Email
{
  public class DbEmailMapper : IDbEmailMapper
  {
    public DbEmail Map(
      ISendEmailRequest request)
    {
      if (request is null)
      {
        return null;
      }

      return new DbEmail
      {
        Id = Guid.NewGuid(),
        SenderId = request.SenderId,
        Receiver = request.Receiver,
        CreatedAtUtc = DateTime.UtcNow
      };
    }
  }
}
