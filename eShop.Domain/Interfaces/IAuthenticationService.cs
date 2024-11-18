using eShop.Domain.DTOs;
using eShop.Domain.Requests.Auth;

namespace eShop.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<ResponseDto> LoginAsync(LoginRequest loginRequestDto);
        public ValueTask<ResponseDto> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<ResponseDto> ChangePersonalDataAsync(ChangePersonalDataRequest changePersonalDataRequestDto);
        public ValueTask<ResponseDto> GetPersonalDataAsync(string Email);
        public ValueTask<ResponseDto> RequestResetPasswordAsync(ResetPasswordRequest request);
        public ValueTask<ResponseDto> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest confirmPasswordResetRequest);
        public ValueTask<ResponseDto> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<ResponseDto> GetExternalProvidersAsync();
        public ValueTask<ResponseDto> LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
        public ValueTask<ResponseDto> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
        public ValueTask<ResponseDto> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest);
        public ValueTask<ResponseDto> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
        public ValueTask<ResponseDto> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest);
        public ValueTask<ResponseDto> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request);
        public ValueTask<ResponseDto> GetTwoFactorStateAsync(string Email);
        public ValueTask<ResponseDto> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public ValueTask<ResponseDto> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest);
        public ValueTask<ResponseDto> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest);
        public ValueTask<ResponseDto> GetPhoneNumber(string Email);
    }
}
