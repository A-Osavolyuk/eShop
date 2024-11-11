namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ChangePasswordCommand(ChangePasswordRequest Request) : IRequest<Result<ChangePasswordResponse>>;

    internal sealed class ChangePasswordCommandHandler(
        AppManager appManager,
        IValidator<ChangePasswordRequest> validator,
        ILogger<ChangePasswordCommandHandler> logger)
        : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly IValidator<ChangePasswordRequest> validator = validator;
        private readonly ILogger<ChangePasswordCommandHandler> logger = logger;

        async Task<Result<ChangePasswordResponse>>
            IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>.Handle(ChangePasswordCommand request,
                CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("change password of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation(
                    "Attempting to change password of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogInformationWithException<ChangePasswordResponse>(
                        new FailedValidationException(validationResult.Errors),
                        actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);
                if (user is null)
                {
                    return logger.LogInformationWithException<ChangePasswordResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."),
                        actionMessage, request.Request.RequestId);
                }

                var isCorrectPassword =
                    await appManager.UserManager.CheckPasswordAsync(user, request.Request.OldPassword);
                if (!isCorrectPassword)
                {
                    return logger.LogInformationWithException<ChangePasswordResponse>(
                        new BadRequestException($"Wrong password."),
                        actionMessage, request.Request.RequestId);
                }

                var result = await appManager.UserManager.ChangePasswordAsync(user, request.Request.OldPassword,
                    request.Request.NewPassword);
                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<ChangePasswordResponse>(
                        new FailedOperationException($"Cannot change password due to server error: {result.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Password was successfully changed. Request ID {requestId}",
                    request.Request.RequestId);
                return new(new ChangePasswordResponse() { Message = "Password has been successfully changed." });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangePasswordResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}