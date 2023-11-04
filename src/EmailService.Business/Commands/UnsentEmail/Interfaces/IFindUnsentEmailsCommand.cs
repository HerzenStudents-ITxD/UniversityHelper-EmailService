using System.Threading.Tasks;
using HerzenHelper.EmailService.Models.Dto.Models;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Requests;
using HerzenHelper.Core.Responses;

namespace HerzenHelper.EmailService.Business.Commands.UnsentEmail.Interfaces
{
  [AutoInject]
  public interface IFindUnsentEmailsCommand
  {
    Task<FindResultResponse<UnsentEmailInfo>> ExecuteAsync(BaseFindFilter filter);
  }
}
