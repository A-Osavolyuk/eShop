namespace eShop.AuthApi.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser?> FindByIdAsync(this UserManager<AppUser> userManager, Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        return user;
    }

    public static async Task<LockoutStatus> GetLockoutStatusAsync(this UserManager<AppUser> userManager, AppUser user)
    {
        var lockoutEnabled = await userManager.GetLockoutEnabledAsync(user);
        var lockoutEnd = await userManager.GetLockoutEndDateAsync(user);

        return new LockoutStatus() { LockoutEnabled = lockoutEnabled, LockoutEnd = lockoutEnd };
    }

    public static async Task<IdentityResult> UnlockUserAsync(this UserManager<AppUser> userManager, AppUser user)
    {
        var lockoutEnabled = await userManager.SetLockoutEnabledAsync(user, false);
        var lockoutEnd = await userManager.SetLockoutEndDateAsync(user, new DateTime(1980, 1, 1));

        return IdentityResult.Success;
    }

    public static string GenerateRandomPassword(this UserManager<AppUser> userManager, int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+";
        StringBuilder sb = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(validChars.Length);
            sb.Append(validChars[randomIndex]);
        }

        return sb.ToString();
    }
    
    public static async Task<IdentityResult> RemoveFromRolesAsync(this UserManager<AppUser> userManager, AppUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        if (roles.Any())
        {
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return result;
            }
        }

        return IdentityResult.Success;
    }
}