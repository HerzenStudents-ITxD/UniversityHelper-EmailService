using System;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Business.Commands.ModuleSetting.Interfaces;
using HerzenHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using HerzenHelper.Core.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HerzenHelper.EmailService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ModuleSettingController : ControllerBase
  {
    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditModuleSettingCommand command,
      [FromQuery] Guid moduleSettingId,
      [FromBody] JsonPatchDocument<EditModuleSettingRequest> patch)
    {
      return await command.ExecuteAsync(moduleSettingId, patch);
    }
  }
}
