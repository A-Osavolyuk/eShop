using eShop.Infrastructure.Account;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Interfaces;
using eShop.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;

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

        public ValueTask<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/login", Method: ApiMethod.POST, Data: loginRequestDto));

        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequest) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/register", Method: ApiMethod.POST, Data: registrationRequest));

        public async ValueTask LogOutAsync()
        {
            await tokenProvider.RemoveTokenAsync();
            (authenticationState as ApplicationAuthenticationStateProvider)!.UpdateAuthenticationState("");

        }
    }
}
