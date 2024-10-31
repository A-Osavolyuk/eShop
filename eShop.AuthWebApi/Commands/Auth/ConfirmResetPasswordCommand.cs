using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmResetPasswordCommand(ConfirmResetPasswordRequest Request)
        : IRequest<Result<ConfirmResetPasswordResponse>>;

    public class ConfirmResetPasswordCommandHandler(
        AppManager appManager,
        ILogger<ConfirmChangeEmailCommandHandler> logger,
        IValidator<ConfirmResetPasswordRequest> validator)
        : IRequestHandler<ConfirmResetPasswordCommand, Result<ConfirmResetPasswordResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ConfirmChangeEmailCommandHandler> logger = logger;
        private readonly IValidator<ConfirmResetPasswordRequest> validator = validator;

        public async Task<Result<ConfirmResetPasswordResponse>> Handle(ConfirmResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage =
                new ActionMessage("confirm reset password of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation(
                    "Attempting to confirm reset password of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogInformationWithException<ConfirmResetPasswordResponse>(
                        new FailedValidationException(validationResult.Errors),
                        actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<ConfirmResetPasswordResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."),
                        actionMessage, request.Request.RequestId);
                }

                var token = Uri.UnescapeDataString(request.Request.ResetToken);
                var resetResult =
                    await appManager.UserManager.ResetPasswordAsync(user, token, request.Request.NewPassword);

                if (!resetResult.Succeeded)
                {
                    return logger.LogErrorWithException<ConfirmResetPasswordResponse>(
                        new FailedOperationException(
                            $"Cannot reset password for user with email {request.Request.Email} " +
                            $"due to server error: {resetResult.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully reset password of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                return new(new ConfirmResetPasswordResponse()
                    { Message = "Your password has been successfully reset." });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ConfirmResetPasswordResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}