using System.Threading.Tasks;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.Core.Attributes;

namespace HerzenHelper.EmailService.Data.Interfaces
{
  [AutoInject]
  public interface IEmailRepository
  {
    Task SaveEmailAsync(DbEmail dbEmail);
  }
}
