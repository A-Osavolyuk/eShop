﻿using eShop.Auth.Api.Data.Entities;
using eShop.Auth.Api.Interfaces;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace eShop.Auth.Api.Queries.Auth;

internal sealed record HandleExternalLoginResponseQuery(
    ExternalLoginInfo ExternalLoginInfo,
    string? RemoteError,
    string? ReturnUri) : IRequest<Result<string>>;

internal sealed class HandleExternalLoginResponseQueryHandler(
    AppManager appManager,
    ITokenHandler tokenHandler,
    IConfiguration configuration,
    IMessageService messageService) : IRequestHandler<HandleExternalLoginResponseQuery, Result<string>>
{
    private readonly AppManager appManager = appManager;
    private readonly ITokenHandler tokenHandler = tokenHandler;
    private readonly IConfiguration configuration = configuration;
    private readonly IMessageService messageService = messageService;
    private readonly string frontendUri = configuration["Configuration:General:Frontend:Clients:BlazorServer:Uri"]!;
    private readonly string defaultRole = configuration["Configuration:General:DefaultValues:DefaultRole"]!;
    private readonly string defaultPermission = configuration["Configuration:General:DefaultValues:DefaultPermission"]!;

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
            var userDto = new User(user.Email!, user.UserName!, user.Id);
            var securityToken = await appManager.SecurityManager.FindTokenAsync(user);

            if (securityToken is not null)
            {
                var tokens = tokenHandler.RefreshToken(securityToken.Token);

                var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri,
                    new { tokens!.AccessToken, tokens.RefreshToken, request.ReturnUri });
                return new(link);
            }
            else
            {
                var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                var permissions = (await appManager.PermissionManager.GetUserPermissionsAsync(user)).ToList();
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

            var tempPassword = appManager.SecurityManager.GenerateRandomPassword(18);
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
                await appManager.PermissionManager.IssuePermissionsAsync(user, [defaultPermission]);

            if (!issuingPermissionsResult.Succeeded)
            {
                return new(new FailedOperationException(
                    $"Cannot assign permissions for user with email {user.Email} " +
                    $"due to server error: {issuingPermissionsResult.Errors.First().Description}"));
            }

            await messageService.SendMessageAsync("external-provider-registration", new ExternalRegistrationMessage()
            {
                To = email,
                Subject = $"Account registered with {request.ExternalLoginInfo!.ProviderDisplayName}",
                TempPassword = tempPassword,
                UserName = email,
                ProviderName = request.ExternalLoginInfo!.ProviderDisplayName!
            });

            var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
            var permissions = (await appManager.PermissionManager.GetUserPermissionsAsync(user)).ToList();
            var token = await tokenHandler.GenerateTokenAsync(user, roles, permissions);
            var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri,
                new { Token = token, ReturnUri = request.ReturnUri });
            return new(link);
        }
    }
}