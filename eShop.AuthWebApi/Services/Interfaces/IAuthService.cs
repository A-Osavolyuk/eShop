namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public ValueTask<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        public ValueTask<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<Result<ChangePersonalDataResponse>> ChangePersonalDataAsync(string UserId, ChangePersonalDataRequest changePersonalDataRequest);
        public ValueTask<Result<PersonalData>> GetPersonalDataAsync(string UserId);
        public ValueTask<Result<ChangePasswordResponse>> ChangePassword(string UserId, ChangePasswordRequest changePasswordRequest);
        public ValueTask<Result<ResetPasswordResponse>> RequestResetPassword(string UserEmail);
        public ValueTask<Result<ConfirmPasswordResetResponse>> ConfirmResetPassword(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
        public ValueTask<Result<LanguageExt.Unit>> ConfirmEmail(string Email, ConfirmEmailRequest confirmEmailRequest);
    }
}
