using System.Threading.Tasks;
using UniversityHelper.EmailService.Data.Interfaces;
using UniversityHelper.EmailService.Data.Provider;
using UniversityHelper.EmailService.Models.Db;

namespace UniversityHelper.EmailService.Data;

public class EmailRepository : IEmailRepository
{
  private readonly IDataProvider _provider;

  public EmailRepository(IDataProvider provider)
  {
    _provider = provider;
  }

  public async Task SaveEmailAsync(DbEmail dbEmail)
  {
    _provider.Emails.Add(dbEmail);
    await _provider.SaveAsync();
  }
}
