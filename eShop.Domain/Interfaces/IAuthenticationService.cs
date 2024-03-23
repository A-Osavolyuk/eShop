using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequest);
        public ValueTask LogOutAsync();
        public ValueTask<ResponseDto> ChangePersonalDataAsync(string Id, ChangePersonalDataRequestDto changePersonalDataRequestDto);
        public ValueTask<ResponseDto> GetPersonalDataAsync(string Id);
        public ValueTask<ResponseDto> ResetPasswordRequestAsync(string Email);
        public ValueTask<ResponseDto> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
    }
}
