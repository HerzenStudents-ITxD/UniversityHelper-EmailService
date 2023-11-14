using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.Core.Enums;
using UniversityHelper.EmailService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.EmailService.Data.Provider;

[AutoInject(InjectType.Scoped)]
public interface IDataProvider : IBaseDataProvider
{
  DbSet<DbEmail> Emails { get; set; }
  DbSet<DbUnsentEmail> UnsentEmails { get; set; }
  DbSet<DbModuleSetting> ModuleSettings { get; set; }
}
