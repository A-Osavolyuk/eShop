namespace eShop.AuthWebApi.Commands.Auth
{
    public record LoginCommand(LoginRequest Request) : IRequest<Result<LoginResponse>>;

    public class LoginCommandHandler(
        IValidator<LoginRequest> validator,
        ILogger<LoginCommandHandler> logger,
        AppManager appManager,
        IEmailSender emailSender,
        ITokenHandler tokenHandler) : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IValidator<LoginRequest> validator = validator;
        private readonly ILogger<LoginCommandHandler> logger = logger;
        private readonly AppManager appManager = appManager;
        private readonly IEmailSender emailSender = emailSender;
        private readonly ITokenHandler tokenHandler = tokenHandler;

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("login user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to login user with email {email}. Request ID {requestId}", request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<LoginResponse>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<LoginResponse>(new NotFoundUserByEmailException(request.Request.Email), actionMessage, request.Request.RequestId);
                }

                if (!user.EmailConfirmed)
                {
                    return logger.LogErrorWithException<LoginResponse>(new InvalidLoginAttemptWithNotConfirmedEmailException(),
                        actionMessage, request.Request.RequestId);
                }

                var isValidPassword = await appManager.UserManager.CheckPasswordAsync(user, request.Request.Password);

                if (!isValidPassword)
                {
                    return logger.LogErrorWithException<LoginResponse>(new InvalidLoginAttemptException(), actionMessage, request.Request.RequestId);
                }

                var userDto = new UserDTO(user.Email!, user.UserName!, user.Id);

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
                var token = tokenHandler.GenerateToken(user, roles);

                logger.LogInformation("Successfully logged in user with email {email}. Request ID {requestID}",
                    request.Request.Email, request.Request.RequestId);
                return new(new LoginResponse()
                {
                    User = userDto,
                    Token = token,
                    Message = "Successfully logged in.",
                    HasTwoFactorAuthentication = false
                });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<LoginResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
