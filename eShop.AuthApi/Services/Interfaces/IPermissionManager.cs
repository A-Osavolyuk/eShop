using eShop.Domain.Entities.AuthApi;

namespace eShop.AuthApi.Services.Interfaces;

internal interface IPermissionManager
{
    public ValueTask<bool> UserHasPermissionAsync(AppUser user, string name);
    public ValueTask<IList<PermissionEntity>> GetPermissionsAsync();
    public ValueTask<IList<string>> GetUserPermisisonsAsync(AppUser user);
    public ValueTask<PermissionEntity?> FindPermissionAsync(string name);
    public ValueTask<IdentityResult> IssuePermissionsToUserAsync(AppUser user, IList<string> permissions);
    public ValueTask<IdentityResult> IssuePermissionToUserAsync(AppUser user, string permission);
    public ValueTask<IdentityResult> RemoveUserFromPermissionAsync(AppUser user, PermissionEntity permissionEntity);
    public ValueTask<IdentityResult> RemoveUserFromPermissionsAsync(AppUser user);
}