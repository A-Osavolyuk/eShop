namespace eShop.AuthApi.Services.Interfaces;

public interface ISecurityManager
{
    public ValueTask<int> GenerateVerifyEmailCodeAsync(string email);
}