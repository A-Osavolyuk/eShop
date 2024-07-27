namespace eShop.AuthWebApi
{
    public sealed class AppManager(
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        public SignInManager<AppUser> SignInManager = signInManager;
        public UserManager<AppUser> UserManager = userManager;
        public RoleManager<IdentityRole> RoleManager = roleManager;
    }
}
