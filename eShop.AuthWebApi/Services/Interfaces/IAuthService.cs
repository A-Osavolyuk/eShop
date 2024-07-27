using eShop.Domain.DTOs.Requests.Cart;
using eShop.Domain.DTOs.Responses.Cart;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public ValueTask<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        public ValueTask<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest);
        public ValueTask<Result<ChangePersonalDataResponse>> ChangePersonalDataAsync(ChangePersonalDataRequest changePersonalDataRequest);
        public ValueTask<Result<PersonalDataResponse>> GetPersonalDataAsync(string Email);
        public ValueTask<Result<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
        public ValueTask<Result<ResetPasswordResponse>> RequestResetPasswordAsync(string UserEmail);
        public ValueTask<Result<ConfirmResetPasswordResponse>> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest confirmPasswordResetRequest);
        public ValueTask<Result<ConfirmEmailResponse>> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest);
        public ValueTask<Result<ChangeTwoFactorAuthenticationResponse>> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request);
        public ValueTask<Result<TwoFactorAuthenticationStateResponse>> GetTwoFactorAuthenticationStateAsync(string Email);
        public ValueTask<Result<LoginResponse>> LoginWithTwoFactorAuthenticationCodeAsync(TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
        public ValueTask<Result<string>> HandleExternalLoginResponseAsync(ExternalLoginInfo externalLoginInfo, string ReturnUri);
        public ValueTask<Result<ExternalLoginResponse>> RequestExternalLogin(string provider, string? returnUri);
        public ValueTask<Result<IEnumerable<ExternalProviderDto>>> GetExternalProvidersAsync();
        public ValueTask<Result<ChangeEmailResponse>> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
        public ValueTask<Result<ConfirmChangeEmailResponse>> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest confirmChangeEmailRequest);
        public ValueTask<Result<ChangeUserNameResponse>> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest);
        public Result<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public ValueTask<Result<ChangePhoneNumberResponse>> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest);
        public ValueTask<Result<ConfirmChangePhoneNumberResponse>> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest);
        public ValueTask<Result<GetPhoneNumberResponse>> GetPhoneNumberAsync(string Email);
        public ValueTask<Result<UserExistsResponse>> UserExistsAsync(UserExistsRequest userExistsRequest);
    }
}
