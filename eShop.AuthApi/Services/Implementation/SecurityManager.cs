namespace eShop.AuthApi.Services.Implementation;

internal sealed class SecurityManager(AuthDbContext context) : ISecurityManager
{
    private readonly AuthDbContext context = context;

    public async ValueTask<int> GenerateVerifyEmailCodeAsync(string email)
    {
        var code = new Random().Next(100000, 999999);
        
        await context.Codes.AddAsync(new CodeEntity()
        {
            Id = Guid.CreateVersion7(),
            Code = code,
            CodeType = CodeType.VerifyEmail,
            SentTo = email,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        });
        
        await context.SaveChangesAsync();
        
        return code;
    }
}