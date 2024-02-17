using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using LanguageExt.Common;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public ValueTask<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest);
        public ValueTask<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequest);
    }
}
