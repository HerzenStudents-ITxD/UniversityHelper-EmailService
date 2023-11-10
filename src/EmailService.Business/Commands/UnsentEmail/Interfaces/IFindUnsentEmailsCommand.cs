using System.Threading.Tasks;
using UniversityHelper.EmailService.Models.Dto.Models;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Requests;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.EmailService.Business.Commands.UnsentEmail.Interfaces
{
  [AutoInject]
  public interface IFindUnsentEmailsCommand
  {
    Task<FindResultResponse<UnsentEmailInfo>> ExecuteAsync(BaseFindFilter filter);
  }
}
