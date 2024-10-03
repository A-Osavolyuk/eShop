namespace eShop.AuthWebApi.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<TUser?> FindByIdAsync<TUser>(this UserManager<TUser> userManager, Guid Id) where TUser : class
        {
            var user = await userManager.FindByIdAsync(Id.ToString());
            return user;
        }

        public static async Task<LockoutStatus> GetLockoutStatusAsync<TUser>(this UserManager<TUser> userManager, TUser user) where TUser : class
        {
            var lockoutEnabled = await userManager.GetLockoutEnabledAsync(user);
            var lockoutEnd = await userManager.GetLockoutEndDateAsync(user);

            return new LockoutStatus() { LockoutEnabled = lockoutEnabled, LockoutEnd = lockoutEnd };
        }

        public static async Task<IdentityResult> UnlockUserAsync<TUser>(this UserManager<TUser> userManager, TUser user) where TUser : class
        {
            var lockoutEnabled = await userManager.SetLockoutEnabledAsync(user, false);
            var lockoutEnd = await userManager.SetLockoutEndDateAsync(user, new DateTime(1980, 1, 1));

            return IdentityResult.Success;
        }
    }
}
