using System;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Business.Commands.UnsentEmail.Interfaces;
using UniversityHelper.EmailService.Models.Dto.Models;
using UniversityHelper.Core.Requests;
using UniversityHelper.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.EmailService.Controllers
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

