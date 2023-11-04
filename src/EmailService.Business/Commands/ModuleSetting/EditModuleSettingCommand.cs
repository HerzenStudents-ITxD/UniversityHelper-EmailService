using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Business.Commands.ModuleSetting.Interfaces;
using HerzenHelper.EmailService.Data.Interfaces;
using HerzenHelper.EmailService.Mappers.Patch.Interfaces;
using HerzenHelper.EmailService.Models.Dto.Requests.ModuleSetting;
//using HerzenHelper.EmailService.Validation.Validators.ModuleSetting.Interfaces;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.FluentValidationExtensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using Microsoft.AspNetCore.JsonPatch;
using HerzenHelper.EmailService.Models.Dto.Helpers;
using HerzenHelper.EmailService.Models.Db;

namespace HerzenHelper.EmailService.Business.Commands.ModuleSetting
{
  public class EditModuleSettingCommand : IEditModuleSettingCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    //private readonly IEditModuleSettingRequestValidator _validator;
    private readonly ISmtpSettingsRepository _repository;
    private readonly IPatchDbModuleSettingMapper _mapper;

    public EditModuleSettingCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      //IEditModuleSettingRequestValidator validator,
      ISmtpSettingsRepository repository,
      IPatchDbModuleSettingMapper mapper)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      //_validator = validator;
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(
      Guid moduleSettingId,
      JsonPatchDocument<EditModuleSettingRequest> patch)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      //if (!_validator.ValidateCustom(patch, out List<string> errors))
      //{
      //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      //}

      OperationResultResponse<bool> response = new();

      response.Body = await _repository.EditAsync(moduleSettingId, _mapper.Map(patch));

      if (!response.Body)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }
      DbModuleSetting dbModuleSetting = await _repository.GetAsync();

      SmtpCredentials.SetSmtpValue(
        dbModuleSetting.Host,
        dbModuleSetting.Port,
        dbModuleSetting.EnableSsl,
        dbModuleSetting.Email,
        dbModuleSetting.Password);
      
      return response;
    }
  }
}
