﻿namespace eShop.AuthApi.Commands.Auth;

internal sealed record LoginCommand(LoginRequest Request) : IRequest<Result<LoginResponse>>;

internal sealed class LoginCommandHandler(
    AppManager appManager,
    IMessageService messageService,
    ITokenHandler tokenHandler,
    AuthDbContext context) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IMessageService messageService = messageService;
    private readonly ITokenHandler tokenHandler = tokenHandler;
    private readonly AuthDbContext context = context;

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        if (!user.EmailConfirmed)
        {
            return new(new BadRequestException("The email address is not confirmed."));
        }

        var isValidPassword = await appManager.UserManager.CheckPasswordAsync(user, request.Request.Password);

        if (!isValidPassword)
        {
            return new(new BadRequestException("The password is not valid."));
        }

        var userDto = new UserDto(user.Email!, user.UserName!, user.Id);
        var securityToken = await appManager.SecurityManager.FindTokenAsync(user);

        if (securityToken is not null)
        {
            var tokens = tokenHandler.RefreshToken(securityToken.Token);

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

                await messageService.SendMessageAsync("2fa-code", new TwoFactorAuthenticationCodeMessage()
                {
                    To = user.Email!,
                    Subject = "Login with 2FA code",
                    UserName = user.UserName!,
                    Code = loginCode
                });

                return new(new LoginResponse()
                {
                    User = userDto,
                    Message = "We have sent an email with 2FA code at your email address.",
                    HasTwoFactorAuthentication = true
                });
            }

            var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
            var permissions = await appManager.PermissionManager.GetUserPermissionsAsync(user);
            var tokens = await tokenHandler.GenerateTokenAsync(user, roles, permissions);

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
}