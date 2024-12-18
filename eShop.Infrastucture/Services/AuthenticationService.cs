using eShop.Domain.Common.Api;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services
{
    public class AuthenticationService(
        IHttpClientService clientService,
        IConfiguration configuration) : IAuthenticationService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<Response> LoginAsync(LoginRequest loginRequestDto) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/login", Method: HttpMethods.POST, Data: loginRequestDto));

        public async ValueTask<Response> RegisterAsync(RegistrationRequest registrationRequest) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/register", Method: HttpMethods.POST, Data: registrationRequest));

        public async ValueTask<Response> ChangePersonalDataAsync(ChangePersonalDataRequest changePersonalDataRequestDto) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-personal-data",
                        Method: HttpMethods.PUT, Data: changePersonalDataRequestDto));

        public async ValueTask<Response> RequestResetPasswordAsync(ResetPasswordRequest request) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/request-reset-password",
                        Method: HttpMethods.POST, Data: request));

        public async ValueTask<Response> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest confirmPasswordResetRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-reset-password",
                        Method: HttpMethods.PUT, Data: confirmPasswordResetRequest));

        public async ValueTask<Response> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-email",
                        Method: HttpMethods.POST, Data: confirmEmailRequest));

        public async ValueTask<Response> GetPersonalDataAsync(string email) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-personal-data/{email}", Method: HttpMethods.GET));

        public async ValueTask<Response> GetExternalProvidersAsync() => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-external-providers", Method: HttpMethods.GET));

        public async ValueTask<Response> LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest) => 
            await clientService.SendAsync(new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/2fa-login",Method: HttpMethods.POST, 
                Data: twoFactorAuthenticationLoginRequest));

        public async ValueTask<Response> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/request-change-email",
                        Method: HttpMethods.PUT, Data: changeEmailRequest));

        public async ValueTask<Response> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-change-email",
                        Method: HttpMethods.POST, Data: changeEmailRequest));

        public async ValueTask<Response> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-password",
                        Method: HttpMethods.PUT, Data: changePasswordRequest));

        public async ValueTask<Response> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-user-name",
                        Method: HttpMethods.PUT, Data: changeUserNameRequest));

        public async ValueTask<Response> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-2fa-state", Method: HttpMethods.POST, Data: request));

        public async ValueTask<Response> GetTwoFactorStateAsync(string email) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-2fa-state/{email}",
                        Method: HttpMethods.GET));

        public async ValueTask<Response> RefreshToken(RefreshTokenRequest refreshTokenRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/refresh-token",
                        Method: HttpMethods.POST, Data: refreshTokenRequest));

        public async ValueTask<Response> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/request-change-phone-number",
                        Method: HttpMethods.PUT, Data: changePhoneNumberRequest));
        public async ValueTask<Response> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest changePhoneNumberRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-change-phone-number",
                        Method: HttpMethods.POST, Data: changePhoneNumberRequest));

        public async ValueTask<Response> GetPhoneNumber(string email) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-phone-number/{email}",
                        Method: HttpMethods.GET));
    }
}
