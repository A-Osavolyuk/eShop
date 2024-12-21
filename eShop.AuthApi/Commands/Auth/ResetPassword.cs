namespace eShop.AuthApi.Commands.Auth;

internal sealed record RequestResetPasswordCommand(ResetPasswordRequest Request)
    : IRequest<Result<ResetPasswordResponse>>;

internal sealed class RequestResetPasswordCommandHandler(
    AppManager appManager,
    IEmailSender emailSender,
    IConfiguration configuration) : IRequestHandler<RequestResetPasswordCommand, Result<ResetPasswordResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IEmailSender emailSender = emailSender;
    private readonly IConfiguration configuration = configuration;
    private readonly string frontendUri = configuration["Configuration:General:Frontend:Clients:BlazorServer:Uri"]!;

    public async Task<Result<ResetPasswordResponse>> Handle(RequestResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var token = await appManager.UserManager.GeneratePasswordResetTokenAsync(user);

        var encodedToken = Uri.EscapeDataString(token);
        var link = UrlGenerator.ActionLink("/account/confirm-password-reset", frontendUri,
            new { Email = request.Request.Email, Token = encodedToken });

        await emailSender.SendResetPasswordMessage(new ResetPasswordMessage()
        {
            To = request.Request.Email,
            Subject = "Reset Password Request",
            Link = link,
            UserName = user.UserName!
        });

        return new(new ResetPasswordResponse()
        {
            Message = $"You have to confirm password reset. " +
                      $"We have sent an email with instructions to your email address."
        });
    }
}