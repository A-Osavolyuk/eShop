namespace eShop.AuthApi.Commands.Auth;

internal sealed record ConfirmEmailCommand(VerifyEmailRequest Request) : IRequest<Result<ConfirmEmailResponse>>;

internal sealed class ConfirmEmailCommandHandler(
    AppManager appManager,
    IEmailSender emailSender,
    CartClient client) : IRequestHandler<ConfirmEmailCommand, Result<ConfirmEmailResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IEmailSender emailSender = emailSender;
    private readonly CartClient client = client;

    public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var confirmResult = await appManager.SecurityManager.VerifyEmailAsync(request.Request.Email, request.Request.Code);

        if (!confirmResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot confirm email address of user with email {request.Request.Email} " +
                $"due to server error: {confirmResult.Errors.First().Description}."));
        }

        await emailSender.SendAccountRegisteredMessage(new AccountRegisteredMessage()
        {
            To = request.Request.Email,
            Subject = "Successful Account Registration",
            UserName = user.UserName!
        });

        var response = await client.InitiateUserAsync(new InitiateUserRequest() { UserId = user.Id });

        if (!response.IsSucceeded)
        {
            return new(new FailedRpcException(response.Message));
        }

        return new(new ConfirmEmailResponse() { Message = "Your email address was successfully confirmed." });
    }
}