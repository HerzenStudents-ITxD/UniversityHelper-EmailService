using System;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Core.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.EmailService.Data.Interfaces
{
  [AutoInject]
  public interface ISmtpSettingsRepository
  {
    Task<bool> CreateAsync(DbModuleSetting dbModuleSetting);

    Task<DbModuleSetting> GetAsync();

    Task<bool> EditAsync(Guid moduleSettingId, JsonPatchDocument<DbModuleSetting> patch);
  }
}
