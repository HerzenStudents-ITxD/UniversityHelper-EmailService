using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.EFSupport.Provider;
using HerzenHelper.Core.Enums;
using HerzenHelper.EmailService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace HerzenHelper.EmailService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbEmail> Emails { get; set; }
    DbSet<DbUnsentEmail> UnsentEmails { get; set; }
    DbSet<DbModuleSetting> ModuleSettings { get; set; }
  }
}
