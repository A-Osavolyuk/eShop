using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Domain.Requests.Auth;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class AuthenticationService(
        IHttpClientService clientService,
        IConfiguration configuration) : IAuthenticationService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> LoginAsync(LoginRequest loginRequestDto) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/login", Method: HttpMethods.POST, Data: loginRequestDto));

        public async ValueTask<ResponseDTO> RegisterAsync(RegistrationRequest registrationRequest) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/register", Method: HttpMethods.POST, Data: registrationRequest));

        public async ValueTask<ResponseDTO> ChangePersonalDataAsync(ChangePersonalDataRequest changePersonalDataRequestDto) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-personal-data",
                        Method: HttpMethods.POST, Data: changePersonalDataRequestDto));

        public async ValueTask<ResponseDTO> RequestResetPasswordAsync(ResetPasswordRequest request) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/request-reset-password",
                        Method: HttpMethods.POST, Data: request));

        public async ValueTask<ResponseDTO> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest confirmPasswordResetRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-reset-password",
                        Method: HttpMethods.POST, Data: confirmPasswordResetRequest));

        public async ValueTask<ResponseDTO> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-email",
                        Method: HttpMethods.POST, Data: confirmEmailRequest));

        public async ValueTask<ResponseDTO> GetPersonalDataAsync(string Email) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-personal-data/{Email}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetExternalProvidersAsync() => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-external-providers", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest) => 
            await clientService.SendAsync(new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/2fa-login",Method: HttpMethods.POST, 
                Data: twoFactorAuthenticationLoginRequest));

        public async ValueTask<ResponseDTO> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/request-change-email",
                        Method: HttpMethods.POST, Data: changeEmailRequest));

        public async ValueTask<ResponseDTO> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-change-email",
                        Method: HttpMethods.POST, Data: changeEmailRequest));

        public async ValueTask<ResponseDTO> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-password",
                        Method: HttpMethods.POST, Data: changePasswordRequest));

        public async ValueTask<ResponseDTO> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-user-name",
                        Method: HttpMethods.POST, Data: changeUserNameRequest));

        public async ValueTask<ResponseDTO> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/change-2fa-state", Data: request, Method: HttpMethods.POST));

        public async ValueTask<ResponseDTO> GetTwoFactorStateAsync(string Email) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-2fa-state/{Email}",
                        Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> RefreshToken(RefreshTokenRequest refreshTokenRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/refresh-token",
                        Method: HttpMethods.POST, Data: refreshTokenRequest));

        public async ValueTask<ResponseDTO> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/request-change-phone-number",
                        Method: HttpMethods.POST, Data: changePhoneNumberRequest));
        public async ValueTask<ResponseDTO> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest changePhoneNumberRequest) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/confirm-change-phone-number",
                        Method: HttpMethods.POST, Data: changePhoneNumberRequest));

        public async ValueTask<ResponseDTO> GetPhoneNumber(string Email) => await clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Auth/get-phone-number/{Email}",
                        Method: HttpMethods.GET));
    }
}
