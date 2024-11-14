using eShop.AuthApi.Data;
using eShop.AuthApi.Services.Interfaces;

namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record LoginCommand(LoginRequest Request) : IRequest<Result<LoginResponse>>;

    internal sealed class LoginCommandHandler(
        IValidator<LoginRequest> validator,
        ILogger<LoginCommandHandler> logger,
        AppManager appManager,
        IEmailSender emailSender,
        ITokenHandler tokenHandler,
        AuthDbContext context) : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IValidator<LoginRequest> validator = validator;
        private readonly ILogger<LoginCommandHandler> logger = logger;
        private readonly AppManager appManager = appManager;
        private readonly IEmailSender emailSender = emailSender;
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly AuthDbContext context = context;

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("login user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to login user with email {email}. Request ID {requestId}", request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogInformationWithException<LoginResponse>(
                        new FailedValidationException(validationResult.Errors), 
                        actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<LoginResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."), 
                        actionMessage, request.Request.RequestId);
                }

                if (!user.EmailConfirmed)
                {
                    return logger.LogErrorWithException<LoginResponse>(
                        new BadRequestException("The email address is not confirmed."),
                        actionMessage, request.Request.RequestId);
                }

                var isValidPassword = await appManager.UserManager.CheckPasswordAsync(user, request.Request.Password);

                if (!isValidPassword)
                {
                    return logger.LogErrorWithException<LoginResponse>(
                        new BadRequestException("The password is not valid."), 
                        actionMessage, request.Request.RequestId);
                }

                var userDto = new UserDto(user.Email!, user.UserName!, user.Id);
                var securityToken = await context.UserAuthenticationTokens.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

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
                    if (user.TwoFactorEnabled)
                    {
                        var loginCode = await appManager.UserManager.GenerateTwoFactorTokenAsync(user, "Email");

                        await emailSender.SendTwoFactorAuthenticationCodeMessage(new TwoFactorAuthenticationCodeMessage()
                        {
                            To = user.Email!,
                            Subject = "Login with 2FA code",
                            UserName = user.UserName!,
                            Code = loginCode
                        });

                        logger.LogInformation("Successfully sent an email with 2FA code to user email {email}. Request ID {requestId}",
                            request.Request.Email, request.Request.RequestId);
                        return new(new LoginResponse()
                        {
                            User = userDto,
                            Message = "We have sent an email with 2FA code at your email address.",
                            HasTwoFactorAuthentication = true
                        });
                    }

                    var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                    var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                    var tokens = await tokenHandler.GenerateTokenAsync(user, roles, permissions);

                    logger.LogInformation("Successfully logged in user with email {email}. Request ID {requestID}",
                        request.Request.Email, request.Request.RequestId);
                    return new(new LoginResponse()
                    {
                        User = userDto,
                        AccessToken = tokens.AccessToken,
                        RefreshToken = tokens.RefreshToken,
                        Message = "Successfully logged in.",
                        HasTwoFactorAuthentication = false
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
