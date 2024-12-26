﻿using eShop.Domain.Models;

namespace eShop.AuthApi.Services.Implementation;

internal sealed class SecurityManager(
    AuthDbContext context,
    UserManager<AppUser> userManager) : ISecurityManager
{
    private readonly AuthDbContext context = context;
    private readonly UserManager<AppUser> userManager = userManager;

    public string GenerateRandomPassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+";
        var sb = new StringBuilder();
        var random = new Random();

        for (var i = 0; i < length; i++)
        {
            var randomIndex = random.Next(validChars.Length);
            sb.Append(validChars[randomIndex]);
        }

        return sb.ToString();
    }
    
    public async ValueTask<string> GenerateVerificationCodeAsync(string sentTo, CodeType codeType)
    {
        var code = GenerateCode();
        await SaveCodeAsync(code, sentTo, codeType);
        return code;
    }

    public async ValueTask<CodeSet> GenerateVerificationCodeSetAsync(DestinationSet destinationSet, CodeType codeType)
    {
        var codeSet = new CodeSet()
        {
            Current = await GenerateVerificationCodeAsync(destinationSet.Current, codeType),
            Next = await GenerateVerificationCodeAsync(destinationSet.Next, codeType)
        };

        return codeSet;
    }

    public async ValueTask<IdentityResult> VerifyEmailAsync(AppUser user, string code)
    {
        var validationResult = await ValidateAndRemoveAsync(code, user.Email!, CodeType.VerifyEmail);
        
        if (!validationResult.Succeeded)
        {
            return validationResult;
        }

        var result = await userManager.ConfirmEmailAsync(user);

        if (!result.Succeeded)
        {
            return result;
        }
        
        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> VerifyPhoneNumberAsync(AppUser user, string code)
    {
        var validationResult = await ValidateAndRemoveAsync(code, user.Email!, CodeType.VerifyEmail);
        
        if (!validationResult.Succeeded)
        {
            return validationResult;
        }

        var result = await userManager.ConfirmPhoneNumberAsync(user);

        if (!result.Succeeded)
        {
            return result;
        }

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> ResetPasswordAsync(AppUser user, string code, string password)
    {
        var validationResult = await ValidateAndRemoveAsync(code, user.Email!, CodeType.VerifyEmail);
        
        if (!validationResult.Succeeded)
        {
            return validationResult;
        }

        var result = await userManager.ResetPasswordAsync(user, password);

        if (!result.Succeeded)
        {
            return result;
        }

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> ChangeEmailAsync(AppUser user, string newEmail, CodeSet codeSet)
    {
        var destinationSet = new DestinationSet() { Current = user.Email!, Next = newEmail };
        var validationResult = await ValidateAndRemoveAsync(codeSet, destinationSet , CodeType.ChangeEmail);
        
        if (!validationResult.Succeeded)
        {
            return validationResult;
        }
        
        var result = await userManager.ChangeEmailAsync(user, newEmail);
        return result;
    }

    public async ValueTask<IdentityResult> ChangePhoneNumberAsync(AppUser user, string newPhoneNumber, CodeSet codeSet)
    {
        var destinationSet = new DestinationSet() { Current = user.PhoneNumber!, Next = newPhoneNumber };
        var validationResult = await ValidateAndRemoveAsync(codeSet, destinationSet , CodeType.ChangePhoneNumber);
        
        if (!validationResult.Succeeded)
        {
            return validationResult;
        }
        
        var result = await userManager.ChangePhoneNumberAsync(user, newPhoneNumber);
        return result;
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

    private async Task RemoveCodeAsync(CodeEntity entity)
    {
        context.Codes.Remove(entity);
        await context.SaveChangesAsync();
    }

    private async Task<IdentityResult> ValidateAndRemoveAsync(string code, string sentTo, CodeType codeType)
    {
        var entity = await FindCodeAsync(code, sentTo, codeType);

        if (entity is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "404",
                Description = "Cannot find code"
            });
        }
        
        if (entity.ExpiresAt < DateTime.UtcNow)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "400",
                Description = $"Code is already expired"
            });
        }
        
        await RemoveCodeAsync(entity);

        return IdentityResult.Success;
    }

    private async Task<IdentityResult> ValidateAndRemoveAsync(CodeSet codeSet, DestinationSet destinationSet, CodeType codeType)
    {
        var currentResult = await ValidateAndRemoveAsync(codeSet.Current, destinationSet.Current, codeType);
        var nextResult = await ValidateAndRemoveAsync(codeSet.Next, destinationSet.Next, codeType);

        if (!currentResult.Succeeded)
        {
            return currentResult;
        }
        
        if (!nextResult.Succeeded)
        {
            return nextResult;
        }

        return IdentityResult.Success;
    }

    #endregion
}