namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ChangeEmailCommand(ChangeEmailRequest Request) : IRequest<Result<ChangeEmailResponse>>;

    internal sealed class RequestChangeEmailCommandHandler(
        AppManager appManager,
        IEmailSender emailSender,
        IConfiguration configuration) : IRequestHandler<ChangeEmailCommand, Result<ChangeEmailResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

        public async Task<Result<ChangeEmailResponse>> Handle(ChangeEmailCommand request,
            CancellationToken cancellationToken)
        {
            var user = await appManager.UserManager.FindByEmailAsync(request.Request.CurrentEmail);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with email {request.Request.CurrentEmail}"));
            }

            var token = await appManager.UserManager.GenerateChangeEmailTokenAsync(user, request.Request.NewEmail);

            var encodedToken = Uri.EscapeDataString(token);
            var link = UrlGenerator.ActionLink("/account/change-email", frontendUri, new
            {
                request.Request.CurrentEmail,
                request.Request.NewEmail,
                Token = encodedToken
            });

            await emailSender.SendChangeEmailMessage(new ChangeEmailMessage()
            {
                Link = link,
                To = request.Request.CurrentEmail,
                Subject = "Change email address request",
                UserName = request.Request.CurrentEmail,
                NewEmail = request.Request.NewEmail,
            });

            return new(new ChangeEmailResponse()
            {
                Message = "We have sent an email with instructions to your email."
            });
        }
    }
}