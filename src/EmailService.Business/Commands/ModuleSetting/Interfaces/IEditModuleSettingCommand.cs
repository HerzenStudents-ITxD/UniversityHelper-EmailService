using System;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.EmailService.Business.Commands.ModuleSetting.Interfaces
{
  [AutoInject]
  public interface IEditModuleSettingCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(
      Guid moduleSettingId,
      JsonPatchDocument<EditModuleSettingRequest> patch);
  }
}
