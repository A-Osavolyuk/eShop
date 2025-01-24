﻿using eShop.Domain.Requests.Api.Auth;
using eShop.Domain.Responses.Api.Auth;

namespace eShop.Auth.Api.Commands.Auth;

internal sealed record VerifyEmailCommand(VerifyEmailRequest Request) : IRequest<Result<VerifyEmailResponse>>;

internal sealed class VerifyEmailCommandHandler(
    AppManager appManager,
    IMessageService messageService,
    CartClient client) : IRequestHandler<VerifyEmailCommand, Result<VerifyEmailResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IMessageService messageService = messageService;
    private readonly CartClient client = client;

    public async Task<Result<VerifyEmailResponse>> Handle(VerifyEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var confirmResult = await appManager.SecurityManager.VerifyEmailAsync(user, request.Request.Code);

        if (!confirmResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot confirm email address of user with email {request.Request.Email} " +
                $"due to server error: {confirmResult.Errors.First().Description}."));
        }

        await messageService.SendMessageAsync("email-verified", new EmailVerifiedMessage()
        {
            To = request.Request.Email,
            Subject = "Email verified",
            UserName = user.UserName!
        });

        var response = await client.InitiateUserAsync(new InitiateUserRequest() { UserId = user.Id });

        if (!response.IsSucceeded)
        {
            return new(new FailedRpcException(response.Message));
        }

        return new(new VerifyEmailResponse() { Message = "Your email address was successfully confirmed." });
    }
}