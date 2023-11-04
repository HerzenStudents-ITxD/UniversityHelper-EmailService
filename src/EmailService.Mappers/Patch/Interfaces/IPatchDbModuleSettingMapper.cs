using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using HerzenHelper.Core.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.EmailService.Mappers.Patch.Interfaces
{
  [AutoInject]
  public interface IPatchDbModuleSettingMapper
  {
    JsonPatchDocument<DbModuleSetting> Map(
      JsonPatchDocument<EditModuleSettingRequest> request);
  }
}
