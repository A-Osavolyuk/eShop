using eShop.Domain.Common.Api;
using eShop.Domain.Requests.AuthApi.Auth;

namespace eShop.Domain.Interfaces;

public interface IAuthenticationService
{
    public ValueTask<Response> LoginAsync(LoginRequest loginRequestDto);
    public ValueTask<Response> RegisterAsync(RegistrationRequest registrationRequest);
    public ValueTask<Response> ChangePersonalDataAsync(ChangePersonalDataRequest changePersonalDataRequestDto);
    public ValueTask<Response> GetPersonalDataAsync(string Email);
    public ValueTask<Response> RequestResetPasswordAsync(ResetPasswordRequest request);
    public ValueTask<Response> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest confirmPasswordResetRequest);
    public ValueTask<Response> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest);
    public ValueTask<Response> GetExternalProvidersAsync();
    public ValueTask<Response> LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest);
    public ValueTask<Response> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
    public ValueTask<Response> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest changeEmailRequest);
    public ValueTask<Response> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    public ValueTask<Response> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest);
    public ValueTask<Response> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request);
    public ValueTask<Response> GetTwoFactorStateAsync(string Email);
    public ValueTask<Response> RefreshToken(RefreshTokenRequest refreshTokenRequest);
    public ValueTask<Response> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest);
    public ValueTask<Response> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest);
    public ValueTask<Response> GetPhoneNumber(string Email);
}