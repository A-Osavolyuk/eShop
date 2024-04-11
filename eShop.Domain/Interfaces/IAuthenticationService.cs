using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<ResponseDTO> LoginAsync(LoginRequest loginRequestDto);
        public ValueTask<ResponseDTO> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<ResponseDTO> ChangePersonalDataAsync(string Email, ChangePersonalDataRequest changePersonalDataRequestDto);
        public ValueTask<ResponseDTO> GetPersonalDataAsync(string Email);
        public ValueTask<ResponseDTO> RequestResetPasswordAsync(string Email);
        public ValueTask<ResponseDTO> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
        public ValueTask<ResponseDTO> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<ResponseDTO> GetExternalProvidersAsync();
        public ValueTask<ResponseDTO> LoginWithTwoFactorAuthenticationAsync(string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
        public ValueTask<ResponseDTO> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
        public ValueTask<ResponseDTO> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest);
        public ValueTask<ResponseDTO> ChangePasswordAsync(string Email, ChangePasswordRequest changePasswordRequest);
        public ValueTask<ResponseDTO> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest);
        public ValueTask<ResponseDTO> ChangeTwoFactorStateAsync(string Email);
        public ValueTask<ResponseDTO> GetTwoFactorStateAsync(string Email);
        public ValueTask<ResponseDTO> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public ValueTask<ResponseDTO> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest);
        public ValueTask<ResponseDTO> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest);
    }
}
