using eShop.AuthApi.Services.Interfaces;
using eShop.AuthApi.Utilities;

namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ChangeEmailCommand(ChangeEmailRequest Request) : IRequest<Result<ChangeEmailResponse>>;

    internal sealed class RequestChangeEmailCommandHandler(
        IValidator<ChangeEmailRequest> validator,
        AppManager appManager,
        ILogger<RequestChangeEmailCommandHandler> logger,
        IEmailSender emailSender,
        IConfiguration configuration) : IRequestHandler<ChangeEmailCommand, Result<ChangeEmailResponse>>
    {
        private readonly IValidator<ChangeEmailRequest> validator = validator;
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RequestChangeEmailCommandHandler> logger = logger;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;
        public async Task<Result<ChangeEmailResponse>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("generate change email address token for user with email {0}", request.Request.CurrentEmail);
            try
            {
                logger.LogInformation("Attempting to generate change email address token for user with email {email}. Request ID {requestId}",
                    request.Request.CurrentEmail, request.Request.RequestId);

                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogInformationWithException<ChangeEmailResponse>(
                        new FailedValidationException(validationResult.Errors), 
                        actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.CurrentEmail);

                if (user is null)
                {
                    return logger.LogInformationWithException<ChangeEmailResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.CurrentEmail}"), 
                        actionMessage, request.Request.RequestId);
                }

                var token = await appManager.UserManager.GenerateChangeEmailTokenAsync(user, request.Request.NewEmail);

                logger.LogInformation("Successfully generated change email address token for user with email {email}. Request ID {requestId}",
                    request.Request.CurrentEmail, request.Request.RequestId);

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

                logger.LogInformation("Successfully sent an email with confirmation of changing email address of user with email {email}. Request ID {requestId}",
                    request.Request.CurrentEmail, request.Request.RequestId);

                return new(new ChangeEmailResponse()
                {
                    Message = "We have sent an email with instructions to your email."
                });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangeEmailResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
