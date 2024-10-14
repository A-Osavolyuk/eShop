namespace eShop.AuthWebApi.Commands.Auth
{
    public record TwoFactorAuthenticationLoginCommand(TwoFactorAuthenticationLoginRequest Request) : IRequest<Result<LoginResponse>>;

    public class TwoFactorAuthenticationLoginCommandHandler(
        ILogger<TwoFactorAuthenticationLoginCommandHandler> logger,
        AppManager appManager,
        ITokenHandler tokenHandler) : IRequestHandler<TwoFactorAuthenticationLoginCommand, Result<LoginResponse>>
    {
        private readonly ILogger<TwoFactorAuthenticationLoginCommandHandler> logger = logger;
        private readonly AppManager appManager = appManager;
        private readonly ITokenHandler tokenHandler = tokenHandler;

        public async Task<Result<LoginResponse>> Handle(TwoFactorAuthenticationLoginCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("login with 2FA code to account with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to login with 2FA code to account with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<LoginResponse>(new NotFoundUserByEmailException(request.Request.Email), actionMessage, request.Request.RequestId);
                }

                var result = await appManager.UserManager.VerifyTwoFactorTokenAsync(user, "Email", request.Request.Code);

                if (!result)
                {
                    return logger.LogErrorWithException<LoginResponse>(new InvalidTwoFactorAuthenticationCodeException(), actionMessage, request.Request.RequestId);
                }

                var userDto = new UserDTO(user.Email!, user.UserName!, user.Id);
                var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                var tokens = await tokenHandler.GenerateTokenAsync(user, roles, permissions);

                logger.LogInformation("Successfully logged in with 2FA code to account with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                return new(new LoginResponse()
                {
                    User = userDto,
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                    Message = "Successfully logged in."
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<LoginResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
