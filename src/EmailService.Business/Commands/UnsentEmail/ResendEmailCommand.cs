﻿using System;
using System.Net;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Broker.Helpers;
using UniversityHelper.EmailService.Business.Commands.UnsentEmail.Interfaces;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Constants;
using UniversityHelper.Core.Enums;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.EmailService.Business.Commands.UnsentEmail;

public class ResendEmailCommand : IResendEmailCommand
{
  private readonly IAccessValidator _accessValidator;
  private readonly EmailSender _emailSender;
  private readonly IResponseCreator _responseCreator;

  public ResendEmailCommand(
    IAccessValidator accessValidator,
    EmailSender emailSender,
    IResponseCreator responseCreator)
  {
    _accessValidator = accessValidator;
    _emailSender = emailSender;
    _responseCreator = responseCreator;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid id)
  {
    //if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveEmailTemplates))
    //{
    //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    //}

    bool isSuccess = await _emailSender.ResendEmail(id);

    return new()
    {
      //Status = isSuccess ? OperationResultStatusType.FullSuccess : OperationResultStatusType.Failed,
      Body = isSuccess
    };
  }
}
