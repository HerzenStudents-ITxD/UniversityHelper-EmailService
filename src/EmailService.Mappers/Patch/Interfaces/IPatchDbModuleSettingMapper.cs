using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using UniversityHelper.Core.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.EmailService.Mappers.Patch.Interfaces;

[AutoInject]
public interface IPatchDbModuleSettingMapper
{
  JsonPatchDocument<DbModuleSetting> Map(
    JsonPatchDocument<EditModuleSettingRequest> request);
}
