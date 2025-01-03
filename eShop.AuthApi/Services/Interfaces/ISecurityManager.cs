namespace eShop.AuthApi.Services.Interfaces;

public interface ISecurityManager
{
    public string GenerateRandomPassword(int length);
    public ValueTask<string> GenerateVerificationCodeAsync(string sentTo, VerificationCodeType verificationCodeType);
    public ValueTask<CodeSet> GenerateVerificationCodeSetAsync(DestinationSet destinationSet, VerificationCodeType verificationCodeType);
    public ValueTask<IdentityResult> VerifyEmailAsync(AppUser user, string code);
    public ValueTask<IdentityResult> VerifyPhoneNumberAsync(AppUser user, string code);
    public ValueTask<IdentityResult> ResetPasswordAsync(AppUser user, string code, string password);
    public ValueTask<IdentityResult> ChangeEmailAsync(AppUser user, string newEmail, CodeSet codeSet);
    public ValueTask<IdentityResult> ChangePhoneNumberAsync(AppUser user, string newPhoneNumber, CodeSet codeSet);
}