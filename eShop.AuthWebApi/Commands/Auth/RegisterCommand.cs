using eShop.AuthWebApi.Utilities;

namespace eShop.AuthWebApi.Commands.Auth
{
    public record RegisterCommand(RegistrationRequest Request) : IRequest<Result<RegistrationResponse>>;

    public class RegisterCommandHandler(
        IValidator<RegistrationRequest> validator,
        AppManager appManager,
        ILogger<RegisterCommandHandler> logger,
        IMapper mapper,
        IEmailSender emailSender,
        IConfiguration configuration) : IRequestHandler<RegisterCommand, Result<RegistrationResponse>>
    {
        private readonly IValidator<RegistrationRequest> validator = validator;
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RegisterCommandHandler> logger = logger;
        private readonly IMapper mapper = mapper;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

        public async Task<Result<RegistrationResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("register account with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to register account with email {email}. Request ID {requestId}", request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (validationResult.IsValid)
                {
                    var exists = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                    if (exists is null)
                    {
                        var user = mapper.Map<AppUser>(request.Request);
                        var registrationResult = await appManager.UserManager.CreateAsync(user, request.Request.Password);

                        if (registrationResult.Succeeded)
                        {
                            logger.LogInformation("Successfully created account with email {email}, waiting for email confirmation. Request ID {requestId}", 
                                request.Request.Email, request.Request.RequestId);

                            var emailConfirmationToken = await appManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
                            var encodedToken = Uri.EscapeDataString(emailConfirmationToken);
                            var link = UrlGenerator.ActionLink("/account/confirm-email", frontendUri,
                                new { Email = request.Request.Email, Token = encodedToken });

                            await emailSender.SendConfirmEmailMessage(new ConfirmEmailMessage()
                            {
                                To = request.Request.Email,
                                Subject = "Email Confirmation",
                                Link = link,
                                UserName = user.UserName!
                            });

                            logger.LogInformation("Successfully sent an email with email address confirmation to email {email}. Request ID {requestId}", 
                                request.Request.Email, request.Request.RequestId);

                            return new(new RegistrationResponse()
                            {
                                Message = $"Your account have been successfully registered. " +
                                $"Now you have to confirm you email address to log in. " +
                                $"We have sent an email with instructions to your email address."
                            });
                        }

                        return logger.LogErrorWithException<RegistrationResponse>(new InvalidRegisterAttemptException(), actionMessage, request.Request.RequestId);
                    }

                    return logger.LogErrorWithException<RegistrationResponse>(new UserAlreadyExistsException(request.Request.Email), actionMessage, request.Request.RequestId);
                }
                return logger.LogErrorWithException<RegistrationResponse>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<RegistrationResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
