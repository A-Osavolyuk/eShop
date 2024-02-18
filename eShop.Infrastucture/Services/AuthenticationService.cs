using eShop.Infrastructure.Account;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Interfaces;
using eShop.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;

namespace eShop.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientService clientService;
        private readonly ITokenProvider tokenProvider;
        private readonly IConfiguration configuration;
        private readonly AuthenticationStateProvider authenticationState;

        public AuthenticationService(
            IHttpClientService clientService, 
            ITokenProvider tokenProvider,
            IConfiguration configuration,
            AuthenticationStateProvider authenticationState)
        {
            this.clientService = clientService;
            this.tokenProvider = tokenProvider;
            this.configuration = configuration;
            this.authenticationState = authenticationState;
        }

        public ValueTask<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/login", Method: ApiMethod.POST, Data: loginRequestDto));

        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequest) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/register", Method: ApiMethod.POST, Data: registrationRequest));

        public async ValueTask LogOut()
        {
            await tokenProvider.RemoveTokenAsync();
            (authenticationState as ApplicationAuthenticationStateProvider)!.UpdateAuthenticationState("");
        }
    }
}
