using eShop.Domain.Models;

namespace eShop.AuthApi.Commands.Auth;

internal sealed record ChangeEmailCommand(ChangeEmailRequest Request) : IRequest<Result<ChangeEmailResponse>>;

internal sealed class RequestChangeEmailCommandHandler(
    AppManager appManager,
    IEmailSender emailSender,
    IConfiguration configuration) : IRequestHandler<ChangeEmailCommand, Result<ChangeEmailResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IEmailSender emailSender = emailSender;
    private readonly IConfiguration configuration = configuration;
    private readonly string frontendUri = configuration["Configuration:General:Frontend:Clients:BlazorServer:Uri"]!;

    public async Task<Result<ChangeEmailResponse>> Handle(ChangeEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.CurrentEmail);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.CurrentEmail}"));
        }

        var destination = new DestinationSet()
        {
            Current = request.Request.CurrentEmail, 
            Next = request.Request.NewEmail
        };
        var code = await appManager.SecurityManager.GenerateVerificationCodeSetAsync(destination, CodeType.ChangeEmail);

        await emailSender.SendMessageAsync("email-change", new ChangeEmailMessage()
        {
            Code = code.Current,
            To = request.Request.CurrentEmail,
            Subject = "Email change (step one)",
            UserName = request.Request.CurrentEmail,
            NewEmail = request.Request.NewEmail,
        });

        await emailSender.SendMessageAsync("new-email-verification", new NewEmailVerification()
        {
            Code = code.Next,
            UserName = request.Request.CurrentEmail,
            Subject = "Email change (step two)",
            To = request.Request.NewEmail,
        });

        return new(new ChangeEmailResponse()
        {
            Message = "We have sent a letter with instructions to your current and new email addresses"
        });
    }
}