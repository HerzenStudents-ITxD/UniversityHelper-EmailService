using System.Collections.Generic;
using System.Net.Mail;
using FluentValidation;
using FluentValidation.Validators;
using HerzenHelper.EmailService.Models.Dto.Requests.ModuleSetting;
using HerzenHelper.EmailService.Validation.Validators.ModuleSetting.Interfaces;
using HerzenHelper.Core.Validators;
using Microsoft.AspNetCore.JsonPatch.Operations;
using ServiceStack.FluentValidation.Validators;

namespace HerzenHelper.EmailService.Validation.Validators.ModuleSetting
{
  //public class EditModuleSettingRequestValidator : BaseEditRequestValidator<EditModuleSettingRequest>, IEditModuleSettingRequestValidator
  //{
  //  private void HandleInternalPropertyValidation(Operation<EditModuleSettingRequest> requestedOperation, CustomContext context)
  //  {
  //    Context = context;
  //    RequestedOperation = requestedOperation;

  //    #region Paths

  //    AddCorrectPaths(
  //      new List<string>
  //      {
  //        nameof(EditModuleSettingRequest.Host),
  //        nameof(EditModuleSettingRequest.Port),
  //        nameof(EditModuleSettingRequest.EnableSsl),
  //        nameof(EditModuleSettingRequest.Email),
  //        nameof(EditModuleSettingRequest.Password)
  //      });

  //    AddCorrectOperations(nameof(EditModuleSettingRequest.Host), new List<OperationType> { OperationType.Replace });
  //    AddCorrectOperations(nameof(EditModuleSettingRequest.Port), new List<OperationType> { OperationType.Replace });
  //    AddCorrectOperations(nameof(EditModuleSettingRequest.EnableSsl), new List<OperationType> { OperationType.Replace });
  //    AddCorrectOperations(nameof(EditModuleSettingRequest.Email), new List<OperationType> { OperationType.Replace });
  //    AddCorrectOperations(nameof(EditModuleSettingRequest.Password), new List<OperationType> { OperationType.Replace });

  //    #endregion

  //    #region Host

  //    AddFailureForPropertyIfNot(
  //      nameof(EditModuleSettingRequest.Host),
  //      x => x == OperationType.Replace,
  //      new()
  //      {
  //        { x => !string.IsNullOrWhiteSpace(x.value?.ToString()), "Host must not be empty." },
  //      });

  //    #endregion

  //    #region Port

  //    AddFailureForPropertyIfNot(
  //      nameof(EditModuleSettingRequest.Port),
  //      x => x == OperationType.Replace,
  //      new()
  //      {
  //        { x => int.TryParse(x.value?.ToString(), out int _), "Incorrect format of Port." },
  //      });

  //    #endregion

  //    #region EnableSsl

  //    AddFailureForPropertyIfNot(
  //      nameof(EditModuleSettingRequest.EnableSsl),
  //      x => x == OperationType.Replace,
  //      new()
  //      {
  //        { x => bool.TryParse(x.value?.ToString(), out bool _), "Incorrect format of EnableSsl." },
  //      });

  //    #endregion

  //    #region Email

  //    AddFailureForPropertyIfNot(
  //      nameof(EditModuleSettingRequest.Email),
  //      x => x == OperationType.Replace,
  //      new()
  //      {
  //        { x =>
  //          {
  //            try
  //            {
  //              MailAddress address = new(x.value?.ToString().Trim());
  //              return true;
  //            }
  //            catch
  //            {
  //              return false;
  //            }
  //          }, "Incorrect email address."
  //        },
  //      });

  //    #endregion

  //    #region Password

  //    AddFailureForPropertyIfNot(
  //      nameof(EditModuleSettingRequest.Password),
  //      x => x == OperationType.Replace,
  //      new()
  //      {
  //        { x => !string.IsNullOrWhiteSpace(x.value?.ToString()), "Password must not be empty." },
  //      });

  //    #endregion
  //  }

  //  public EditModuleSettingRequestValidator()
  //  {
  //    RuleForEach(x => x.Operations)
  //      .Custom(HandleInternalPropertyValidation);
  //  }
  //}
}
