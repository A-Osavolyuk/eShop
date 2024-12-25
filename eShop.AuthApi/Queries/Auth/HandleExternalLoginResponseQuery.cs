﻿namespace eShop.AuthApi.Queries.Auth;

internal sealed record HandleExternalLoginResponseQuery(
    ExternalLoginInfo ExternalLoginInfo,
    string? RemoteError,
    string? ReturnUri) : IRequest<Result<string>>;

internal sealed class HandleExternalLoginResponseQueryHandler(
    AppManager appManager,
    ITokenHandler tokenHandler,
    IConfiguration configuration,
    IEmailSender emailSender,
    AuthDbContext context) : IRequestHandler<HandleExternalLoginResponseQuery, Result<string>>
{
    private readonly AppManager appManager = appManager;
    private readonly ITokenHandler tokenHandler = tokenHandler;
    private readonly IConfiguration configuration = configuration;
    private readonly IEmailSender emailSender = emailSender;
    private readonly AuthDbContext context = context;
    private readonly string frontendUri = configuration["Configuration:General:Frontend:Clients:BlazorServer:Uri"]!;
    private readonly string defaultRole = configuration["Configuration:General:DefaultValues:DefaultRole"]!;

    private readonly List<string> defaultPermissions =
        configuration.GetValue<List<string>>("Configuration:General:DefaultValues:DefaultPermissions")!;

    public async Task<Result<string>> Handle(HandleExternalLoginResponseQuery request,
        CancellationToken cancellationToken)
    {
        var email = request.ExternalLoginInfo.Principal.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        if (email is null)
        {
            return new(new BadRequestException("No email address specified in credentials."));
        }

        var user = await appManager.UserManager.FindByEmailAsync(email);

        if (user is not null)
        {
            var userDto = new UserDto(user.Email!, user.UserName!, user.Id);
            var securityToken = await context.SecurityTokens.AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

            if (securityToken is not null)
            {
                var tokens = tokenHandler.ReuseToken(securityToken.Token);

                var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri,
                    new { tokens!.AccessToken, tokens.RefreshToken, request.ReturnUri });
                return new(link);
            }
            else
            {
                var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                var tokens = await tokenHandler.GenerateTokenAsync(user, roles, permissions);
                var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri,
                    new { tokens!.AccessToken, tokens.RefreshToken, request.ReturnUri });
                return new(link);
            }
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
                return new(new FailedOperationException($"Cannot create user account " +
                                                        $"due to server error: {result.Errors.First().Description}"));
            }

            var assignDefaultRoleResult = await appManager.UserManager.AddToRoleAsync(user, defaultRole);

            if (!assignDefaultRoleResult.Succeeded)
            {
                return new(new FailedOperationException(
                    $"Cannot assign role {defaultRole} to user with email {user.Email}" +
                    $"due to server error: {assignDefaultRoleResult.Errors.First().Description}"));
            }

            var issuingPermissionsResult =
                await appManager.PermissionManager.IssuePermissionsToUserAsync(user, defaultPermissions);

            if (!issuingPermissionsResult.Succeeded)
            {
                return new(new FailedOperationException(
                    $"Cannot assing permissions for user with email {user.Email} " +
                    $"due to server error: {issuingPermissionsResult.Errors.First().Description}"));
            }

            await emailSender.SendAccountRegisteredOnExternalLoginMessage(
                new AccountRegisteredOnExternalLoginMessage()
                {
                    To = email,
                    Subject = $"Account created with {request.ExternalLoginInfo!.ProviderDisplayName} sign in",
                    TempPassword = tempPassword,
                    UserName = email,
                    ProviderName = request.ExternalLoginInfo!.ProviderDisplayName!
                });

            var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
            var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
            var token = await tokenHandler.GenerateTokenAsync(user, roles, permissions);
            var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri,
                new { Token = token, ReturnUri = request.ReturnUri });
            return new(link);
        }
    }
}