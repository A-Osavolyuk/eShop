namespace eShop.AuthApi.Commands.Auth;

internal sealed record TwoFactorAuthenticationLoginCommand(TwoFactorAuthenticationLoginRequest Request)
    : IRequest<Result<LoginResponse>>;

internal sealed class TwoFactorAuthenticationLoginCommandHandler(
    AppManager appManager,
    ITokenHandler tokenHandler,
    AuthDbContext context) : IRequestHandler<TwoFactorAuthenticationLoginCommand, Result<LoginResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly ITokenHandler tokenHandler = tokenHandler;
    private readonly AuthDbContext context = context;

    public async Task<Result<LoginResponse>> Handle(TwoFactorAuthenticationLoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var result =
            await appManager.UserManager.VerifyTwoFactorTokenAsync(user, "Email", request.Request.Code);

        if (!result)
        {
            return new(new BadRequestException($"Invalid two-factor code {request.Request.Code}."));
        }

        var userDto = new UserDto(user.Email!, user.UserName!, user.Id);
        var securityToken = await context.SecurityTokens.AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

        if (securityToken is not null)
        {
            var tokens = tokenHandler.ReuseToken(securityToken.Token);
                    
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
            var permissions = (await appManager.PermissionManager.GetUserPermissionsAsync(user)).ToList();
            var tokens = await tokenHandler.GenerateTokenAsync(user, roles, permissions);

            return new(new LoginResponse()
            {
                User = userDto,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                Message = "Successfully logged in."
            });
        }
    }
}