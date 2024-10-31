using eShop.AuthWebApi.Utilities;
using eShop.Domain.Requests.Auth;

namespace eShop.AuthWebApi.Commands.Auth
{
    public record RequestResetPasswordCommand(ResetPasswordRequest Request) : IRequest<Result<ResetPasswordResponse>>;

    public class RequestResetPasswordCommandHandler(
        AppManager appManager,
        ILogger<RequestResetPasswordCommandHandler> logger,
        IEmailSender emailSender,
        IConfiguration configuration) : IRequestHandler<RequestResetPasswordCommand, Result<ResetPasswordResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RequestResetPasswordCommandHandler> logger = logger;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

        public async Task<Result<ResetPasswordResponse>> Handle(RequestResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("generate reset password token for user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to generate reset password token for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<ResetPasswordResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."), 
                        actionMessage, request.Request.RequestId);
                }

                var token = await appManager.UserManager.GeneratePasswordResetTokenAsync(user);

                logger.LogInformation("Successfully generated reset password token for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var encodedToken = Uri.EscapeDataString(token);
                var link = UrlGenerator.ActionLink("/account/confirm-password-reset", frontendUri, new { Email = request.Request.Email, Token = encodedToken });

                await emailSender.SendResetPasswordMessage(new ResetPasswordMessage()
                {
                    To = request.Request.Email,
                    Subject = "Reset Password Request",
                    Link = link,
                    UserName = user.UserName!
                });

                logger.LogInformation("Successfully sent an email with confirmation to reset password for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                return new(new ResetPasswordResponse()
                {
                    Message = $"You have to confirm password reset. " +
                    $"We have sent an email with instructions to your email address."
                });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ResetPasswordResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
