using System;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.EmailService.Business.Commands.ModuleSetting.Interfaces
{
  [AutoInject]
  public interface IEditModuleSettingCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(
      Guid moduleSettingId,
      JsonPatchDocument<EditModuleSettingRequest> patch);
  }
}
