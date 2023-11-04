using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Business.Commands.UnsentEmail.Interfaces;
using HerzenHelper.EmailService.Data.Interfaces;
using HerzenHelper.EmailService.Mappers.Models.Interfaces;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.EmailService.Models.Dto.Models;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.Enums;
using HerzenHelper.Core.FluentValidationExtensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Requests;
using HerzenHelper.Core.Responses;
using HerzenHelper.Core.Validators.Interfaces;

namespace HerzenHelper.EmailService.Business.Commands.UnsentEmail
{
  public class FindUnsentEmailsCommand : IFindUnsentEmailsCommand
  {
    private readonly IAccessValidator _accessValidator;
    //private readonly IBaseFindFilterValidator _baseFindValidator;
    private readonly IUnsentEmailRepository _repository;
    private readonly IUnsentEmailInfoMapper _unsentEmailMapper;
    private readonly IResponseCreator _responseCreator;

    public FindUnsentEmailsCommand(
      IAccessValidator accessValidator,
      //IBaseFindFilterValidator baseFindValidator,
      IUnsentEmailRepository repository,
      IUnsentEmailInfoMapper mapper,
      IResponseCreator responseCreator)
    {
      _accessValidator = accessValidator;
      //_baseFindValidator = baseFindValidator;
      _repository = repository;
      _unsentEmailMapper = mapper;
      _responseCreator = responseCreator;
    }

    public async Task<FindResultResponse<UnsentEmailInfo>> ExecuteAsync(BaseFindFilter filter)
    {
      //if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveEmailTemplates))
      //{
      //  return _responseCreator.CreateFailureFindResponse<UnsentEmailInfo>(HttpStatusCode.Forbidden);
      //}

      //if (!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      //{
      //  return _responseCreator.CreateFailureFindResponse<UnsentEmailInfo>(
      //    HttpStatusCode.BadRequest,
      //    errors);
      //}

      (List<DbUnsentEmail> unsentEmailes, int totalCount) repositoryResponse = await _repository.FindAsync(filter);

      return new()
      {
        Body = repositoryResponse.unsentEmailes?.Select(_unsentEmailMapper.Map).ToList(),
        TotalCount = repositoryResponse.totalCount,
        //Status = OperationResultStatusType.FullSuccess
      };
    }
  }
}
