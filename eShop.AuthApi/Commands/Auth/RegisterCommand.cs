namespace eShop.AuthApi.Commands.Auth;

internal sealed record RegisterCommand(RegistrationRequest Request) : IRequest<Result<RegistrationResponse>>;

internal sealed class RegisterCommandHandler(
    AppManager appManager,
    IEmailSender emailSender,
    IConfiguration configuration) : IRequestHandler<RegisterCommand, Result<RegistrationResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IEmailSender emailSender = emailSender;
    private readonly string frontendUri = configuration["Configuration:General:Frontend:Clients:BlazorServer:Uri"]!;
    private readonly string defaultRole = configuration["Configuration:General:DefaultValues:DefaultRole"]!;

    private readonly List<string> defaultPermissions =
    [
        "Permission.Account.ManageAccount"
    ];

    public async Task<Result<RegistrationResponse>> Handle(RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is not null)
        {
            return new(new BadRequestException("User already exists"));
        }

        var newUser = UserMapper.ToAppUser(request.Request);
        var registrationResult = await appManager.UserManager.CreateAsync(newUser, request.Request.Password);

        if (!registrationResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot create user due to server error: {registrationResult.Errors.First().Description}"));
        }

        var assignDefaultRoleResult = await appManager.UserManager.AddToRoleAsync(newUser, defaultRole);

        if (!assignDefaultRoleResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot assign role {defaultRole} to user with email {newUser.Email} " +
                $"due to server errors: {assignDefaultRoleResult.Errors.First().Description}"));
        }

        var issuingPermissionsResult =
            await appManager.PermissionManager.IssuePermissionsToUserAsync(newUser, defaultPermissions);

        if (!issuingPermissionsResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot issue permissions for user with email {request.Request.Email} " +
                $"due to server errors: {issuingPermissionsResult.Errors.First().Description}"));
        }

        //var emailConfirmationToken = await appManager.UserManager.GenerateEmailConfirmationTokenAsync(newUser);
        var emailVerificationCode = await appManager.SecurityManager.GenerateVerificationCodeAsync(newUser.Email!, CodeType.VerifyEmail);
        //var encodedToken = Uri.EscapeDataString(emailConfirmationToken);
        //var link = UrlGenerator.ActionLink("/account/confirm-email", frontendUri, new { Email = request.Request.Email, Token = encodedToken });

        await emailSender.SendEmailVerificationMessage(new EmailVerificationMessage()
        {
            To = request.Request.Email,
            Subject = "Email verification",
            Code = emailVerificationCode,
            UserName = newUser.UserName!
        });


        return new(new RegistrationResponse()
        {
            Message = $"Your account have been successfully registered. " +
                      $"Now you have to confirm you email address to log in. " +
                      $"We have sent an email with instructions to your email address."
        });
    }
}