using eShop.Infrastructure.Account;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Interfaces;
using eShop.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientService clientService;
        private readonly ITokenProvider tokenProvider;
        private readonly IConfiguration configuration;

        public AuthenticationService(
            IHttpClientService clientService, 
            ITokenProvider tokenProvider,
            IConfiguration configuration)
        {
            this.clientService = clientService;
            this.tokenProvider = tokenProvider;
            this.configuration = configuration;
        }

        public ValueTask<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto) => clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/login", Method: ApiMethod.POST, Data: loginRequestDto));

        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequest) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:AuthWebApiUrl"]}/api/v1/Auth/register", Method: ApiMethod.POST, Data: registrationRequest));

        public async ValueTask LogOut()
        {
            await tokenProvider.RemoveTokenAsync();
            JwtHandler.Token = "";
            
        }
    }
}
