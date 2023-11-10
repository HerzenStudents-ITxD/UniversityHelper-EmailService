using System;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Data.Interfaces;
using UniversityHelper.EmailService.Data.Provider;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.EmailService.Data
{
  public class SmtpSettingsRepository : ISmtpSettingsRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SmtpSettingsRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> CreateAsync(DbModuleSetting dbModuleSetting)
    {
      if (dbModuleSetting is null)
      {
        return false;
      }

      _provider.ModuleSettings.Add(dbModuleSetting);
      await _provider.SaveAsync();

      return true;
    }

    public async Task<bool> EditAsync(Guid moduleSettingId, JsonPatchDocument<DbModuleSetting> patch)
    {
      if (patch is null)
      {
        return false;
      }

      DbModuleSetting dbModuleSetting = await _provider.ModuleSettings
        .FirstOrDefaultAsync(ms => ms.Id == moduleSettingId);

      if (dbModuleSetting is null)
      {
        return false;
      }

      patch.ApplyTo(dbModuleSetting);
      dbModuleSetting.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      dbModuleSetting.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }

    public async Task<DbModuleSetting> GetAsync()
    {
      return await _provider.ModuleSettings.FirstOrDefaultAsync();
    }
  }
}
