using System;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.Core.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.EmailService.Data.Interfaces
{
  [AutoInject]
  public interface ISmtpSettingsRepository
  {
    Task<bool> CreateAsync(DbModuleSetting dbModuleSetting);

    Task<DbModuleSetting> GetAsync();

    Task<bool> EditAsync(Guid moduleSettingId, JsonPatchDocument<DbModuleSetting> patch);
  }
}
