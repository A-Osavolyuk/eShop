namespace eShop.AuthWebApi.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<TUser?> FindByIdAsync<TUser>(this UserManager<TUser> userManager, Guid Id) where TUser : class
        {
            var user = await userManager.FindByIdAsync(Id.ToString());
            return user;
        }
    }
}
