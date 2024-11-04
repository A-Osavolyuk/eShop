namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangeTwoFactorAuthenticationStateCommand(ChangeTwoFactorAuthenticationRequest Request)
        : IRequest<Result<ChangeTwoFactorAuthenticationResponse>>;

    public class ChangeTwoFactorAuthenticationStateCommandHandler(
        AppManager appManager,
        ILogger<ChangeTwoFactorAuthenticationStateCommandHandler> logger)
        : IRequestHandler<ChangeTwoFactorAuthenticationStateCommand, Result<ChangeTwoFactorAuthenticationResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ChangeTwoFactorAuthenticationStateCommandHandler> logger = logger;

        public async Task<Result<ChangeTwoFactorAuthenticationResponse>> Handle(
            ChangeTwoFactorAuthenticationStateCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("change 2fa state of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation(
                    "Attempting to change 2fa state of user with email {email}.Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<ChangeTwoFactorAuthenticationResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."),
                        actionMessage, request.Request.RequestId);
                }

                IdentityResult result = null!;

                result = await appManager.UserManager.SetTwoFactorEnabledAsync(user, !user.TwoFactorEnabled);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<ChangeTwoFactorAuthenticationResponse>(
                        new FailedOperationException(
                            $"Cannot change 2fa state of user with email {request.Request.Email} " +
                            $"due to server error: {result.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
                }

                var state = user.TwoFactorEnabled ? "disabled" : "enabled";

                logger.LogInformation(
                    "Successfully changed 2fa state of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                return new(new ChangeTwoFactorAuthenticationResponse()
                {
                    Message = $"Two factor authentication was successfully {state}.",
                    TwoFactorAuthenticationState = user.TwoFactorEnabled,
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangeTwoFactorAuthenticationResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}