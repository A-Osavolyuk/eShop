using eShop.Domain.Entities.AuthApi;

namespace eShop.AuthApi.Utilities;

internal sealed class AppManager(
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IPermissionManager permissionManager)
{
    public readonly SignInManager<AppUser> SignInManager = signInManager;
    public readonly UserManager<AppUser> UserManager = userManager;
    public readonly RoleManager<IdentityRole> RoleManager = roleManager;
    public readonly IPermissionManager PermissionManager = permissionManager;
}