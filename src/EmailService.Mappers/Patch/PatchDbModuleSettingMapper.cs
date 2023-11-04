using HerzenHelper.EmailService.Mappers.Patch.Interfaces;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace HerzenHelper.EmailService.Mappers.Patch
{
  public class PatchDbModuleSettingMapper : IPatchDbModuleSettingMapper
  {
    public JsonPatchDocument<DbModuleSetting> Map(JsonPatchDocument<EditModuleSettingRequest> request)
    {
      if (request is null)
      {
        return null;
      }

      JsonPatchDocument<DbModuleSetting> dbPatch = new();

      foreach (var item in request.Operations)
      {
        dbPatch.Operations.Add(new Operation<DbModuleSetting>(item.op, item.path, item.from, item.value.ToString().Trim()));
      }

      return dbPatch;
    }
  }
}
