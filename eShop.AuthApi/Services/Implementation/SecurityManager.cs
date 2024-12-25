namespace eShop.AuthApi.Services.Implementation;

internal sealed class SecurityManager(
    AuthDbContext context,
    UserManager<AppUser> userManager) : ISecurityManager
{
    private readonly AuthDbContext context = context;
    private readonly UserManager<AppUser> userManager = userManager;

    public async ValueTask<string> GenerateVerificationCodeAsync(string sentTo, CodeType codeType)
    {
        var code = GenerateCode();

        await SaveCodeAsync(code, sentTo, codeType);

        return code;
    }

    public async ValueTask<IdentityResult> VerifyEmailAsync(AppUser user, string code)
    {
        var entity = await FindCodeAsync(code, user.Email!, CodeType.VerifyEmail);

        if (entity is null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "404", Description = $"Cannot find code" });
        }

        if (entity.ExpiresAt < DateTime.UtcNow)
        {
            return IdentityResult.Failed(new IdentityError()
                { Code = "400", Description = $"Verification code is already expired" });
        }

        user.EmailConfirmed = true;

        context.Codes.Remove(entity);
        await userManager.UpdateAsync(user);
        await context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> VerifyPhoneNumberAsync(AppUser user, string code)
    {
        var entity = await FindCodeAsync(code, user.PhoneNumber!, CodeType.VerifyPhoneNumber);

        if (entity is null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "404", Description = $"Cannot find code" });
        }

        if (entity.ExpiresAt < DateTime.UtcNow)
        {
            return IdentityResult.Failed(new IdentityError()
                { Code = "400", Description = $"Verification code is already expired" });
        }

        user.PhoneNumberConfirmed = true;

        context.Codes.Remove(entity);
        await userManager.UpdateAsync(user);
        await context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> ResetPasswordAsync(AppUser user, string code, string password)
    {
        var entity = await FindCodeAsync(code, user.Email!, CodeType.ResetPassword);
        
        if (entity is null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "404", Description = $"Cannot find code" });
        }

        if (entity.ExpiresAt < DateTime.UtcNow)
        {
            return IdentityResult.Failed(new IdentityError()
                { Code = "400", Description = $"Verification code is already expired" });
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, token, password);

        if (!result.Succeeded)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "500",
                Description = result.Errors.First().Description
            });
        }

        return IdentityResult.Success;
    }

    #region Private methods

    private string GenerateCode()
    {
        var code = new Random().Next(100000, 999999).ToString();
        return code;
    }

    private async Task SaveCodeAsync(string code, string sentTo, CodeType codeType)
    {
        await context.Codes.AddAsync(new CodeEntity()
        {
            Id = Guid.CreateVersion7(),
            SentTo = sentTo,
            Code = code,
            CodeType = codeType,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        });

        await context.SaveChangesAsync();
    }

    private async Task<CodeEntity?> FindCodeAsync(string code, string sentTo, CodeType codeType)
    {
        var entity = await context.Codes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Code == code && c.SentTo == sentTo && c.CodeType == codeType);

        return entity;
    }

    #endregion
}