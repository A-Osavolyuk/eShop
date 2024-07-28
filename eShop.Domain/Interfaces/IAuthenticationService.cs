using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.Requests.Auth;

namespace eShop.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<ResponseDTO> LoginAsync(LoginRequest loginRequestDto);
        public ValueTask<ResponseDTO> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<ResponseDTO> ChangePersonalDataAsync(ChangePersonalDataRequest changePersonalDataRequestDto);
        public ValueTask<ResponseDTO> GetPersonalDataAsync(string Email);
        public ValueTask<ResponseDTO> RequestResetPasswordAsync(ResetPasswordRequest request);
        public ValueTask<ResponseDTO> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest confirmPasswordResetRequest);
        public ValueTask<ResponseDTO> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<ResponseDTO> GetExternalProvidersAsync();
        public ValueTask<ResponseDTO> LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
        public ValueTask<ResponseDTO> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
        public ValueTask<ResponseDTO> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest);
        public ValueTask<ResponseDTO> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
        public ValueTask<ResponseDTO> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest);
        public ValueTask<ResponseDTO> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request);
        public ValueTask<ResponseDTO> GetTwoFactorStateAsync(string Email);
        public ValueTask<ResponseDTO> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public ValueTask<ResponseDTO> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest);
        public ValueTask<ResponseDTO> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest);
        public ValueTask<ResponseDTO> GetPhoneNumber(string Email);
    }
}
