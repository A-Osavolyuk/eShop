﻿using eShop.AuthWebApi.Utilities;
using eShop.Domain.Entities.Auth;

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
        private readonly string defaultRole = configuration["DefaultValues:DeafultRole"]!;
        private readonly List<string> defaultPermissions = configuration.GetValue<List<string>>("DefaultValues:DeafultPermissions")!;

        public async Task<Result<RegistrationResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("register account with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to register account with email {email}. Request ID {requestId}", request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<RegistrationResponse>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is not null)
                {
                    return logger.LogErrorWithException<RegistrationResponse>(new UserAlreadyExistsException(request.Request.Email), actionMessage, request.Request.RequestId);
                }

                var newUser = mapper.Map<AppUser>(request.Request);
                var registrationResult = await appManager.UserManager.CreateAsync(newUser, request.Request.Password);

                if (!registrationResult.Succeeded)
                {
                    return logger.LogErrorWithException<RegistrationResponse>(new InvalidRegisterAttemptException(), actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully created account with email {email}, waiting for email confirmation. Request ID {requestId}",
                        request.Request.Email, request.Request.RequestId);

                var assignDefaultRoleResult = await appManager.UserManager.AddToRoleAsync(newUser, defaultRole);

                if (!assignDefaultRoleResult.Succeeded)
                {
                    return logger.LogErrorWithException<RegistrationResponse>(new NotAssignRoleException(assignDefaultRoleResult.Errors.First().Description), 
                        new("assign default role for user with email {0}", request.Request.Email));
                }

                var issuingPermissionsResult = await appManager.PermissionManager.IssuePermissionsToUserAsync(newUser, defaultPermissions);

                if (!issuingPermissionsResult.Succeeded)
                {
                    return logger.LogErrorWithException<RegistrationResponse>(new NotIssuedPermissionsException(assignDefaultRoleResult.Errors),
                        new("issie default permissions for user with email {0}", newUser.Email));
                }

                var emailConfirmationToken = await appManager.UserManager.GenerateEmailConfirmationTokenAsync(newUser);
                var encodedToken = Uri.EscapeDataString(emailConfirmationToken);
                var link = UrlGenerator.ActionLink("/account/confirm-email", frontendUri,
                    new { Email = request.Request.Email, Token = encodedToken });

                await emailSender.SendConfirmEmailMessage(new ConfirmEmailMessage()
                {
                    To = request.Request.Email,
                    Subject = "Email Confirmation",
                    Link = link,
                    UserName = newUser.UserName!
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
            catch (Exception ex)
            {
                return logger.LogErrorWithException<RegistrationResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
