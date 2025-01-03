namespace eShop.AuthApi.Commands.Auth;

internal sealed record ResendEmailVerificationCodeCommand (ResendEmailVerificationCodeRequest Request)
    : IRequest<Result<ResendEmailVerificationCodeResponse>>;

internal sealed class ResendEmailVerificationCodeCommandHandler(AppManager manager, IMessageService messageService, AuthDbContext context)
    : IRequestHandler<ResendEmailVerificationCodeCommand, Result<ResendEmailVerificationCodeResponse>>
{
    private readonly AppManager manager = manager;
    private readonly IMessageService messageService = messageService;
    private readonly AuthDbContext context = context;

    public async Task<Result<ResendEmailVerificationCodeResponse>> Handle(ResendEmailVerificationCodeCommand request,
        CancellationToken cancellationToken)
    {
        var user = await manager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email: {request.Request.Email}"));
        }

        string code;
        
        var entity = await context.Codes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SentTo == request.Request.Email && x.VerificationCodeType == VerificationCodeType.VerifyEmail, cancellationToken: cancellationToken);

        if (entity is null || entity.ExpiresAt < DateTime.UtcNow)
        {
            code = await manager.SecurityManager.GenerateVerificationCodeAsync(user.Email!, VerificationCodeType.VerifyEmail);
        }
        else
        {
            code = entity.Code;
        }

        await messageService.SendMessageAsync("email-verification", new EmailVerificationMessage()
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