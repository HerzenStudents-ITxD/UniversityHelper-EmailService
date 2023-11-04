using System;
using System.Threading.Tasks;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;

namespace HerzenHelper.EmailService.Business.Commands.UnsentEmail.Interfaces
{
  [AutoInject]
  public interface IResendEmailCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid id);
  }
}
