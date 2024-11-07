using eShop.AuthApi.Data;
using eShop.AuthApi.Services.Interfaces;

namespace eShop.AuthApi.Commands.Auth
{
    public record TwoFactorAuthenticationLoginCommand(TwoFactorAuthenticationLoginRequest Request)
        : IRequest<Result<LoginResponse>>;

    public class TwoFactorAuthenticationLoginCommandHandler(
        ILogger<TwoFactorAuthenticationLoginCommandHandler> logger,
        AppManager appManager,
        ITokenHandler tokenHandler,
        AuthDbContext context) : IRequestHandler<TwoFactorAuthenticationLoginCommand, Result<LoginResponse>>
    {
        private readonly ILogger<TwoFactorAuthenticationLoginCommandHandler> logger = logger;
        private readonly AppManager appManager = appManager;
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly AuthDbContext context = context;

        public async Task<Result<LoginResponse>> Handle(TwoFactorAuthenticationLoginCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage =
                new ActionMessage("login with 2FA code to account with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation(
                    "Attempting to login with 2FA code to account with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<LoginResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."),
                        actionMessage, request.Request.RequestId);
                }

                var result =
                    await appManager.UserManager.VerifyTwoFactorTokenAsync(user, "Email", request.Request.Code);

                if (!result)
                {
                    return logger.LogInformationWithException<LoginResponse>(
                        new BadRequestException($"Invalid two-factor code {request.Request.Code}."),
                        actionMessage, request.Request.RequestId);
                }

                var userDto = new UserDTO(user.Email!, user.UserName!, user.Id);
                var securityToken = await context.UserAuthenticationTokens.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

                if (securityToken is not null)
                {
                    var tokens = tokenHandler.ReuseToken(securityToken.Token);

                    logger.LogInformation("Successfully logged in user with email {email}. Request ID {requestID}",
                        request.Request.Email, request.Request.RequestId);

                    return new(new LoginResponse()
                    {
                        User = userDto,
                        AccessToken = tokens!.AccessToken,
                        RefreshToken = tokens.RefreshToken,
                        Message = "Successfully logged in.",
                        HasTwoFactorAuthentication = false
                    });
                }
                else
                {
                    var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                    var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                    var tokens = await tokenHandler.GenerateTokenAsync(user, roles, permissions);

                    logger.LogInformation(
                        "Successfully logged in with 2FA code to account with email {email}. Request ID {requestId}",
                        request.Request.Email, request.Request.RequestId);

                    return new(new LoginResponse()
                    {
                        User = userDto,
                        AccessToken = tokens.AccessToken,
                        RefreshToken = tokens.RefreshToken,
                        Message = "Successfully logged in."
                    });
                }
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<LoginResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}