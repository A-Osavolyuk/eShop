
using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmResetPasswordCommand(ConfirmResetPasswordRequest Request) : IRequest<Result<ConfirmResetPasswordResponse>>;

    public class ConfirmResetPasswordCommandHandler(
        AppManager appManager,
        ILogger<ConfirmChangeEmailCommandHandler> logger,
        IValidator<ConfirmResetPasswordRequest> validator) : IRequestHandler<ConfirmResetPasswordCommand, Result<ConfirmResetPasswordResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ConfirmChangeEmailCommandHandler> logger = logger;
        private readonly IValidator<ConfirmResetPasswordRequest> validator = validator;

        public async Task<Result<ConfirmResetPasswordResponse>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("confirm reset password of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to confirm reset password of user with email {email}. Request ID {requestId}", 
                    request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request);

                if (validationResult.IsValid)
                {
                    var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                    if (user is not null)
                    {
                        var token = Uri.UnescapeDataString(request.Request.ResetToken);
                        var resetResult = await appManager.UserManager.ResetPasswordAsync(user, token, request.Request.NewPassword);

                        if (resetResult.Succeeded)
                        {
                            logger.LogInformation("Successfully reset password of user with email {email}. Request ID {requestId}",
                                request.Request.Email, request.Request.RequestId);
                            return new(new ConfirmResetPasswordResponse() { Message = "Your password has been successfully reset." });
                        }
                        return logger.LogErrorWithException<ConfirmResetPasswordResponse>(new NotResetPasswordException(), actionMessage, request.Request.RequestId);
                    }

                    return logger.LogErrorWithException<ConfirmResetPasswordResponse>(new NotFoundUserByEmailException(request.Request.Email), 
                        actionMessage, request.Request.RequestId);
                }

                return logger.LogErrorWithException<ConfirmResetPasswordResponse>(new FailedValidationException(validationResult.Errors), 
                    actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ConfirmResetPasswordResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
