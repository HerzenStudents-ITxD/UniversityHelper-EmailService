﻿using System;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Broker.Helpers;
using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.Models.Broker.Requests.Email;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace UniversityHelper.EmailService.Broker.Consumers;

public class SendEmailConsumer : IConsumer<ISendEmailRequest>
{
  private readonly ILogger<SendEmailConsumer> _logger;
  private readonly EmailSender _sender;

  private async Task<bool> SendEmailAsync(ISendEmailRequest request)
  {
    _logger.LogInformation(
      "Start email sending to '{Receiver}'.",
      request.Receiver);

    return request is null
      ? false
      : await _sender.SendEmailAsync(request.Receiver, request.Subject, request.Text, request.SenderId);
  }

  public SendEmailConsumer(
    ILogger<SendEmailConsumer> logger,
    EmailSender sender)
  {
    _logger = logger;
    _sender = sender;
  }

  public async Task Consume(ConsumeContext<ISendEmailRequest> context)
  {
    Object response = OperationResultWrapper.CreateResponse(SendEmailAsync, context.Message);

    await context.RespondAsync<IOperationResult<bool>>(response);
  }
}
