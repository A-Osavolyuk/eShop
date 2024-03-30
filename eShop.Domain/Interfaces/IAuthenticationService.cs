using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<ResponseDto> LoginAsync(LoginRequest loginRequestDto);
        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask LogOutAsync();
        public ValueTask<ResponseDto> ChangePersonalDataAsync(string Id, ChangePersonalDataRequest changePersonalDataRequestDto);
        public ValueTask<ResponseDto> GetPersonalDataAsync(string Id);
        public ValueTask<ResponseDto> RequestResetPasswordAsync(string Email);
        public ValueTask<ResponseDto> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
        public ValueTask<ResponseDto> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<ResponseDto> GetExternalProvidersAsync();
        public ValueTask<ResponseDto> LoginWithTwoFactorAuthenticationAsync(string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
    }
}
