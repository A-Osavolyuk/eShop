using LanguageExt;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public ValueTask<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        public ValueTask<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<Result<ChangePersonalDataResponse>> ChangePersonalDataAsync(string UserId, ChangePersonalDataRequest changePersonalDataRequest);
        public ValueTask<Result<PersonalData>> GetPersonalDataAsync(string UserId);
        public ValueTask<Result<ChangePasswordResponse>> ChangePasswordAsync(string UserId, ChangePasswordRequest changePasswordRequest);
        public ValueTask<Result<ResetPasswordResponse>> RequestResetPasswordAsync(string UserEmail);
        public ValueTask<Result<ConfirmPasswordResetResponse>> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
        public ValueTask<Result<Unit>> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<Result<ChangeTwoFactorAuthenticationResponse>> ChangeTwoFactorAuthenticationStateAsync(string Email);
        public ValueTask<Result<TwoFactorAuthenticationStateResponse>> GetTwoFactorAuthenticationStateAsync(string Email);
        public ValueTask<Result<LoginResponse>> LoginWithTwoFactorAuthenticationCodeAsync(string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
        public ValueTask<Result<string>> HandleExternalLoginResponseAsync(ExternalLoginInfo externalLoginInfo, string ReturnUri);
        public ValueTask<Result<ExternalLoginResponse>> RequestExternalLogin(string provider, string? returnUri);
        public ValueTask<Result<IEnumerable<ExternalProviderDto>>> GetExternalProviders();
        public ValueTask<Result<ChangeEmailResponse>> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
    }
}
