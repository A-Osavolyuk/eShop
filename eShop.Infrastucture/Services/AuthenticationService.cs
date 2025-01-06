using eShop.Domain.Common.Api;
using eShop.Domain.Requests.AuthApi.Auth;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class AuthenticationService(
    IHttpClientService clientService,
    IConfiguration configuration) : IAuthenticationService
{
    private readonly IHttpClientService clientService = clientService;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> LoginAsync(LoginRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/login",
            Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> ResendVerificationCodeAsync(ResendEmailVerificationCodeRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/resend-verification-code",
                Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> VerifyCodeAsync(VerifyCodeRequest request) => await clientService.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/verify-code",
            Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> RegisterAsync(RegistrationRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/register",
            Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> ChangePersonalDataAsync(ChangePersonalDataRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/change-personal-data",
                Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> RequestResetPasswordAsync(ResetPasswordRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/request-reset-password",
                Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/confirm-reset-password",
                Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> VerifyEmailAsync(VerifyEmailRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/verify-email",
            Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> GetPersonalDataAsync(string email) => await clientService.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/get-personal-data/{email}",
            Method: HttpMethods.Get));

    public async ValueTask<Response> GetExternalProvidersAsync() => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/get-external-providers",
            Method: HttpMethods.Get));

    public async ValueTask<Response>
        LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest request) =>
        await clientService.SendAsync(new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/2fa-login",
            Method: HttpMethods.Post,
            Data: request));

    public async ValueTask<Response> RequestChangeEmailAsync(ChangeEmailRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/request-change-email",
                Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/confirm-change-email",
                Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> ChangePasswordAsync(ChangePasswordRequest request) =>
        await clientService.SendAsync(
            new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/change-password",
                Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> ChangeUserNameAsync(ChangeUserNameRequest request) =>
        await clientService.SendAsync(
            new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/change-user-name",
                Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> ChangeTwoFactorAuthenticationStateAsync(
        ChangeTwoFactorAuthenticationRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/change-2fa-state",
            Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> GetTwoFactorStateAsync(string email) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/get-2fa-state/{email}",
            Method: HttpMethods.Get));

    public async ValueTask<Response> RefreshToken(RefreshTokenRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/refresh-token",
            Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/request-change-phone-number",
                Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest request) =>
        await clientService.SendAsync(
            new Request(
                Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/confirm-change-phone-number",
                Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> GetPhoneNumber(string email) => await clientService.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Auth/get-phone-number/{email}",
            Method: HttpMethods.Get));
}