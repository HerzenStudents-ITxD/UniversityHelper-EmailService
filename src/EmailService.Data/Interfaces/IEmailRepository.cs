using System.Threading.Tasks;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.EmailService.Data.Interfaces
{
  [AutoInject]
  public interface IEmailRepository
  {
    Task SaveEmailAsync(DbEmail dbEmail);
  }
}
