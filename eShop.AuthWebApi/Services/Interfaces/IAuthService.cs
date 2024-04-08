using LanguageExt;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public ValueTask<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        public ValueTask<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<Result<ChangePersonalDataResponse>> ChangePersonalDataAsync(string Email, ChangePersonalDataRequest changePersonalDataRequest);
        public ValueTask<Result<PersonalDataDto>> GetPersonalDataAsync(string UserId);
        public ValueTask<Result<ChangePasswordResponse>> ChangePasswordAsync(string Email, ChangePasswordRequest changePasswordRequest);
        public ValueTask<Result<ResetPasswordResponse>> RequestResetPasswordAsync(string UserEmail);
        public ValueTask<Result<ConfirmPasswordResetResponse>> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
        public ValueTask<Result<Unit>> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<Result<ChangeTwoFactorAuthenticationResponse>> ChangeTwoFactorAuthenticationStateAsync(string Email);
        public ValueTask<Result<TwoFactorAuthenticationStateResponse>> GetTwoFactorAuthenticationStateAsync(string Email);
        public ValueTask<Result<LoginResponse>> LoginWithTwoFactorAuthenticationCodeAsync(string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
        public ValueTask<Result<string>> HandleExternalLoginResponseAsync(ExternalLoginInfo externalLoginInfo, string ReturnUri);
        public ValueTask<Result<ExternalLoginResponse>> RequestExternalLogin(string provider, string? returnUri);
        public ValueTask<Result<IEnumerable<ExternalProviderDto>>> GetExternalProvidersAsync();
        public ValueTask<Result<ChangeEmailResponse>> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
        public ValueTask<Result<ConfirmChangeEmailResponse>> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest confirmChangeEmailRequest);
        public ValueTask<Result<ChangeUserNameResponse>> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest);
        public Result<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        public ValueTask<Result<ChangePhoneNumberResponse>> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest);
        public ValueTask<Result<ConfirmChangePhoneNumberResponse>> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest);
    }
}
