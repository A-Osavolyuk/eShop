namespace eShop.AuthApi.Services.Implementation;

internal sealed class SecurityManager(
    AuthDbContext context,
    UserManager<AppUser> userManager) : ISecurityManager
{
    private readonly AuthDbContext context = context;
    private readonly UserManager<AppUser> userManager = userManager;

    public async ValueTask<string> GenerateVerifyEmailCodeAsync(string email)
    {
        var code = new Random().Next(100000, 999999).ToString();

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

    public async ValueTask<string> ResendEmailVerificationCodeAsync(string email)
    {
        var entity = await context.Codes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SentTo == email && x.CodeType == CodeType.VerifyEmail);

        if (entity is null || entity.ExpiresAt < DateTime.UtcNow)
        {
            return await GenerateVerifyEmailCodeAsync(email);
        }
        else
        {
            return entity.Code;
        }
    }

    public async ValueTask<IdentityResult> VerifyEmailAsync(string email, string verificationCode)
    {
        var code = await context.Codes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Code == verificationCode && c.SentTo == email && c.CodeType == CodeType.VerifyEmail);
        
        if (code is null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "404", Description = $"Cannot find code" });
        }

        if (code.ExpiresAt < DateTime.UtcNow)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "400", Description = $"Verification code is already expired" });
        }
        
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "404", Description = $"Cannot find user with email {email}" });
        }
        
        user.EmailConfirmed = true;
        
        context.Codes.Remove(code);
        await userManager.UpdateAsync(user);
        await context.SaveChangesAsync();
        
        return IdentityResult.Success;
    }
}