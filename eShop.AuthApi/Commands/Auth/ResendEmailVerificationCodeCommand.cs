namespace eShop.AuthApi.Commands.Auth;

internal sealed record ResendEmailVerificationCodeCommand (ResendEmailVerificationCodeRequest Request)
    : IRequest<Result<ResendEmailVerificationCodeResponse>>;

internal sealed class ResendEmailVerificationCodeCommandHandler(AppManager manager, IEmailSender emailSender)
    : IRequestHandler<ResendEmailVerificationCodeCommand, Result<ResendEmailVerificationCodeResponse>>
{
    private readonly AppManager manager = manager;
    private readonly IEmailSender emailSender = emailSender;

    public async Task<Result<ResendEmailVerificationCodeResponse>> Handle(ResendEmailVerificationCodeCommand request,
        CancellationToken cancellationToken)
    {
        var user = await manager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email: {request.Request.Email}"));
        }
        
        var code = await manager.SecurityManager.ResendEmailVerificationCodeAsync(request.Request.Email);

        await emailSender.SendEmailVerificationMessage(new EmailVerificationMessage()
        {
            To = request.Request.Email,
            Code = code,
            Subject = "Email verification",
            UserName = user.UserName!
        });
        
        return new Result<ResendEmailVerificationCodeResponse>(new ResendEmailVerificationCodeResponse()
        {
            Message = "Verification code was successfully resend"
        });
    }
}