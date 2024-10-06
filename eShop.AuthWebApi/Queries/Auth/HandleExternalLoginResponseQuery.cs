using eShop.AuthWebApi.Utilities;

namespace eShop.AuthWebApi.Queries.Auth
{
    public record HandleExternalLoginResponseQuery(ExternalLoginInfo ExternalLoginInfo, string? RemoteError, string? ReturnUri) : IRequest<Result<string>>;

    public class HandleExternalLoginResponseQueryHandler(
        AppManager appManager,
        ILogger<HandleExternalLoginResponseQuery> logger,
        ITokenHandler tokenHandler,
        IConfiguration configuration,
        IEmailSender emailSender) : IRequestHandler<HandleExternalLoginResponseQuery, Result<string>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<HandleExternalLoginResponseQuery> logger = logger;
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly IConfiguration configuration = configuration;
        private readonly IEmailSender emailSender = emailSender;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;
        private readonly string defaultRole = configuration["DefaultValues:DeafultRole"]!;
        private readonly List<string> defaultPermissions = configuration.GetValue<List<string>>("DefaultValues:DeafultPermissions")!;

        public async Task<Result<string>> Handle(HandleExternalLoginResponseQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("handle external login response of provider {0}", request.ExternalLoginInfo.LoginProvider);
            try
            {
                logger.LogInformation("Attempting to handle external login response of provider {provider}", request.ExternalLoginInfo.LoginProvider);
                var email = request.ExternalLoginInfo.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;

                if (email is null)
                {
                    return logger.LogErrorWithException<string>(new NullCredentialsException(), actionMessage);
                }

                var user = await appManager.UserManager.FindByEmailAsync(email);

                if (user is not null)
                {
                    logger.LogInformation("Successfully logged in with external provider {provider}", request.ExternalLoginInfo.LoginProvider);
                    var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                    var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                    var token = tokenHandler.GenerateToken(user, roles, permissions);
                    var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri, new { Token = token, ReturnUri = request.ReturnUri });
                    return new(link);
                }
                else
                {
                    user = new AppUser()
                    {
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };

                    var tempPassword = appManager.UserManager.GenerateRandomPassword(18);
                    var result = await appManager.UserManager.CreateAsync(user, tempPassword);

                    if (!result.Succeeded)
                    {
                        return logger.LogErrorWithException<string>(new InvalidRegisterAttemptException(), actionMessage);
                    }

                    var assignDefaultRoleResult = await appManager.UserManager.AddToRoleAsync(user, defaultRole);

                    if (!assignDefaultRoleResult.Succeeded)
                    {
                        return logger.LogErrorWithException<string>(new NotAssignRoleException(assignDefaultRoleResult.Errors.First().Description),
                            new("assign default role for user with email {0}", user.Email));
                    }

                    var issuingPermissionsResult = await appManager.PermissionManager.IssuePermissionsToUserAsync(user, defaultPermissions);

                    if (!issuingPermissionsResult.Succeeded)
                    {
                        return logger.LogErrorWithException<string>(new NotIssuedPermissionsException(assignDefaultRoleResult.Errors),
                            new("issie default permissions for user with email {0}", user.Email));
                    }

                    logger.LogInformation("Successfully created account with email {email} based on external login data from provider {provider}",
                            email, request.ExternalLoginInfo.LoginProvider);

                    await emailSender.SendAccountRegisteredOnExternalLoginMessage(new AccountRegisteredOnExternalLoginMessage()
                    {
                        To = email,
                        Subject = $"Account created with {request.ExternalLoginInfo!.ProviderDisplayName} sign in",
                        TempPassword = tempPassword,
                        UserName = email,
                        ProviderName = request.ExternalLoginInfo!.ProviderDisplayName!
                    });

                    logger.LogInformation("Successfully logged in with external provider {provider}", request.ExternalLoginInfo.LoginProvider);
                    var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                    var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                    var token = tokenHandler.GenerateToken(user, roles, permissions);
                    var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri, new { Token = token, ReturnUri = request.ReturnUri });
                    return new(link);
                }
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<string>(ex, actionMessage);
            }
        }
    }
}
