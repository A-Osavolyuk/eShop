using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequest);
        public ValueTask LogOut();
    }
}
