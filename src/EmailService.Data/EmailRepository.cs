using System.Threading.Tasks;
using HerzenHelper.EmailService.Data.Interfaces;
using HerzenHelper.EmailService.Data.Provider;
using HerzenHelper.EmailService.Models.Db;

namespace HerzenHelper.EmailService.Data
{
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
}
