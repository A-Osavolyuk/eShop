using eShop.Domain.Common.Api;
using eShop.Domain.Requests.Api.Auth;

namespace eShop.Domain.Interfaces.Client;

public interface IAuthenticationService
{
    public ValueTask<Response> LoginAsync(LoginRequest request);
    public ValueTask<Response> RegisterAsync(RegistrationRequest request);
    public ValueTask<Response> ChangePersonalDataAsync(ChangePersonalDataRequest request);
    public ValueTask<Response> GetPersonalDataAsync(string email);
    public ValueTask<Response> RequestResetPasswordAsync(ResetPasswordRequest request);
    public ValueTask<Response> ConfirmResetPasswordAsync(ConfirmResetPasswordRequest request);
    public ValueTask<Response> VerifyEmailAsync(VerifyEmailRequest request);
    public ValueTask<Response> GetExternalProvidersAsync();
    public ValueTask<Response> LoginWithTwoFactorAuthenticationAsync(TwoFactorAuthenticationLoginRequest request);
    public ValueTask<Response> RequestChangeEmailAsync(ChangeEmailRequest request);
    public ValueTask<Response> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest request);
    public ValueTask<Response> ChangePasswordAsync(ChangePasswordRequest request);
    public ValueTask<Response> ChangeUserNameAsync(ChangeUserNameRequest request);
    public ValueTask<Response> ChangeTwoFactorAuthenticationStateAsync(ChangeTwoFactorAuthenticationRequest request);
    public ValueTask<Response> GetTwoFactorStateAsync(string email);
    public ValueTask<Response> RefreshToken(RefreshTokenRequest request);
    public ValueTask<Response> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest request);
    public ValueTask<Response> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest request);
    public ValueTask<Response> GetPhoneNumber(string email);
    public ValueTask<Response> ResendVerificationCodeAsync(ResendEmailVerificationCodeRequest request);
    public ValueTask<Response> VerifyCodeAsync(VerifyCodeRequest request);
}