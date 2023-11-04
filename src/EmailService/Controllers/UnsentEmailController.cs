using System;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Business.Commands.UnsentEmail.Interfaces;
using HerzenHelper.EmailService.Models.Dto.Models;
using HerzenHelper.Core.Requests;
using HerzenHelper.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HerzenHelper.EmailService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class UnsentEmailController : ControllerBase
  {
    [HttpDelete("resend")]
    public async Task<OperationResultResponse<bool>> ResendAsync(
      [FromServices] IResendEmailCommand command,
      [FromQuery] Guid unsentEmailId)
    {
      return await command.ExecuteAsync(unsentEmailId);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<UnsentEmailInfo>> FindAsync(
      [FromServices] IFindUnsentEmailsCommand command,
      [FromQuery] BaseFindFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }
  }
}

