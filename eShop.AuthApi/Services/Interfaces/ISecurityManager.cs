namespace eShop.AuthApi.Services.Interfaces;

public interface ISecurityManager
{
    public ValueTask<string> GenerateVerificationCodeAsync(string sentTo, CodeType codeType);
    public ValueTask<IdentityResult> VerifyEmailAsync(AppUser user, string code);
    public ValueTask<IdentityResult> VerifyPhoneNumberAsync(AppUser user, string code);
    public ValueTask<IdentityResult> ResetPasswordAsync(AppUser user, string code, string password);
}