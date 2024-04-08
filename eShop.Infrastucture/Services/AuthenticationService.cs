using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
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

        public ValueTask<ResponseDto> LoginAsync(LoginRequest loginRequestDto) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/login", Method: ApiMethod.POST, Data: loginRequestDto));

        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequest registrationRequest) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/register", Method: ApiMethod.POST, Data: registrationRequest));

        public ValueTask<ResponseDto> ChangePersonalDataAsync(string Email, ChangePersonalDataRequest changePersonalDataRequestDto) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-personal-data/{Email}",
                        Method: ApiMethod.POST, Data: changePersonalDataRequestDto));

        public ValueTask<ResponseDto> RequestResetPasswordAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/request-reset-password/{Email}",
                        Method: ApiMethod.POST));

        public ValueTask<ResponseDto> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-reset-password/{Email}",
                        Method: ApiMethod.POST, Data: confirmPasswordResetRequest));

        public ValueTask<ResponseDto> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-email/{Email}",
                        Method: ApiMethod.POST, Data: confirmEmailRequest));

        public ValueTask<ResponseDto> GetPersonalDataAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-personal-data/{Email}", Method: ApiMethod.GET));

        public ValueTask<ResponseDto> GetExternalProvidersAsync() => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-external-providers", Method: ApiMethod.GET));

        public ValueTask<ResponseDto> LoginWithTwoFactorAuthenticationAsync(
            string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/2fa-login/{Email}",
                        Method: ApiMethod.POST, Data: twoFactorAuthenticationLoginRequest));

        public ValueTask<ResponseDto> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/request-change-email",
                        Method: ApiMethod.POST, Data: changeEmailRequest));

        public ValueTask<ResponseDto> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-change-email",
                        Method: ApiMethod.POST, Data: changeEmailRequest));

        public ValueTask<ResponseDto> ChangePasswordAsync(string Email, ChangePasswordRequest changePasswordRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-password/{Email}",
                        Method: ApiMethod.POST, Data: changePasswordRequest));

        public ValueTask<ResponseDto> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-user-name",
                        Method: ApiMethod.POST, Data: changeUserNameRequest));

        public ValueTask<ResponseDto> ChangeTwoFactorStateAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-2fa-state/{Email}",
                        Method: ApiMethod.POST));

        public ValueTask<ResponseDto> GetTwoFactorStateAsync(string Email) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-2fa-state/{Email}",
                        Method: ApiMethod.GET));

        public ValueTask<ResponseDto> RefreshToken(RefreshTokenRequest refreshTokenRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/refresh-token",
                        Method: ApiMethod.POST, Data: refreshTokenRequest));

        public ValueTask<ResponseDto> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/request-change-phone-number",
                        Method: ApiMethod.POST, Data: changePhoneNumberRequest));
        public ValueTask<ResponseDto> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest changePhoneNumberRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/confirm-change-phone-number",
                        Method: ApiMethod.POST, Data: changePhoneNumberRequest));
    }
}
