using System;
using UniversityHelper.EmailService.Mappers.Db.Email.Interfaces;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Models.Broker.Requests.Email;

namespace UniversityHelper.EmailService.Mappers.Db.Email;

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
