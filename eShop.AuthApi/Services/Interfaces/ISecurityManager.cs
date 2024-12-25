namespace eShop.AuthApi.Services.Interfaces;

public interface ISecurityManager
{
    public ValueTask<string> GenerateVerifyEmailCodeAsync(string email);
    public ValueTask<string> ResendEmailVerificationCodeAsync(string email);
    public ValueTask<IdentityResult> VerifyEmailAsync(string email, string verificationCode);
}