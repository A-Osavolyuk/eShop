using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class AuthenticationService(
        IHttpClientService clientService,
        IConfiguration configuration) : IAuthenticationService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public ValueTask<ResponseDTO> LoginAsync(LoginRequest loginRequestDto) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/login", Method: HttpMethods.POST, Data: loginRequestDto));

        public ValueTask<ResponseDTO> RegisterAsync(RegistrationRequest registrationRequest) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/register", Method: HttpMethods.POST, Data: registrationRequest));

        public ValueTask<ResponseDTO> ChangePersonalDataAsync(string Email, ChangePersonalDataRequest changePersonalDataRequestDto) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-personal-data/{Email}",
                        Method: HttpMethods.POST, Data: changePersonalDataRequestDto));

        public ValueTask<ResponseDTO> RequestResetPasswordAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/request-reset-password/{Email}",
                        Method: HttpMethods.POST));

        public ValueTask<ResponseDTO> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-reset-password/{Email}",
                        Method: HttpMethods.POST, Data: confirmPasswordResetRequest));

        public ValueTask<ResponseDTO> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-email/{Email}",
                        Method: HttpMethods.POST, Data: confirmEmailRequest));

        public ValueTask<ResponseDTO> GetPersonalDataAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-personal-data/{Email}", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> GetExternalProvidersAsync() => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-external-providers", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> LoginWithTwoFactorAuthenticationAsync(
            string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/2fa-login/{Email}",
                        Method: HttpMethods.POST, Data: twoFactorAuthenticationLoginRequest));

        public ValueTask<ResponseDTO> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/request-change-email",
                        Method: HttpMethods.POST, Data: changeEmailRequest));

        public ValueTask<ResponseDTO> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-change-email",
                        Method: HttpMethods.POST, Data: changeEmailRequest));

        public ValueTask<ResponseDTO> ChangePasswordAsync(string Email, ChangePasswordRequest changePasswordRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-password/{Email}",
                        Method: HttpMethods.POST, Data: changePasswordRequest));

        public ValueTask<ResponseDTO> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-user-name",
                        Method: HttpMethods.POST, Data: changeUserNameRequest));

        public ValueTask<ResponseDTO> ChangeTwoFactorStateAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-2fa-state/{Email}",
                        Method: HttpMethods.POST));

        public ValueTask<ResponseDTO> GetTwoFactorStateAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-2fa-state/{Email}",
                        Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> RefreshToken(RefreshTokenRequest refreshTokenRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/refresh-token",
                        Method: HttpMethods.POST, Data: refreshTokenRequest));

        public ValueTask<ResponseDTO> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/request-change-phone-number",
                        Method: HttpMethods.POST, Data: changePhoneNumberRequest));
        public ValueTask<ResponseDTO> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest changePhoneNumberRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-change-phone-number",
                        Method: HttpMethods.POST, Data: changePhoneNumberRequest));

        public ValueTask<ResponseDTO> GetPhoneNumber(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-phone-number/{Email}",
                        Method: HttpMethods.GET));
    }
}
