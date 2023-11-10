using System;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.EmailService.Business.Commands.UnsentEmail.Interfaces
{
  [AutoInject]
  public interface IResendEmailCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid id);
  }
}
