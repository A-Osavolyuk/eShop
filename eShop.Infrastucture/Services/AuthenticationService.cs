using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Infrastructure.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class AuthenticationService(
        IHttpClientService clientService,
        ITokenProvider tokenProvider,
        IConfiguration configuration,
        AuthenticationStateProvider authenticationState) : IAuthenticationService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly ITokenProvider tokenProvider = tokenProvider;
        private readonly IConfiguration configuration = configuration;
        private readonly AuthenticationStateProvider authenticationState = authenticationState;

        public ValueTask<ResponseDto> LoginAsync(LoginRequest loginRequestDto) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/login", Method: ApiMethod.POST, Data: loginRequestDto));

        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequest registrationRequest) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/register", Method: ApiMethod.POST, Data: registrationRequest));

        public async ValueTask LogOutAsync()
        {
            await tokenProvider.RemoveTokenAsync();
            (authenticationState as ApplicationAuthenticationStateProvider)!.UpdateAuthenticationState("");

        }

        public ValueTask<ResponseDto> ChangePersonalDataAsync(string Id, ChangePersonalDataRequest changePersonalDataRequestDto) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/change-personal-data/{Id}",
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

        public ValueTask<ResponseDto> GetPersonalDataAsync(string Id) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-personal-data/{Id}", Method: ApiMethod.GET));

        public ValueTask<ResponseDto> GetExternalProvidersAsync() => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/get-external-providers", Method: ApiMethod.GET));

        public ValueTask<ResponseDto> LoginWithTwoFactorAuthenticationAsync(
            string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest) => clientService.SendAsync(
                    new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/2fa-login/{Email}",
                        Method: ApiMethod.POST, Data: twoFactorAuthenticationLoginRequest));
    }
}
