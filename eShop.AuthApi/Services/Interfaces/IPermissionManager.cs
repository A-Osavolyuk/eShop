using eShop.Domain.Entities.Admin;

namespace eShop.AuthApi.Services.Interfaces
{
    public interface IPermissionManager
    {
        public ValueTask<bool> UserHasPermissionAsync(AppUser user, string name);
        public ValueTask<IList<Permission>> GetPermissionsAsync();
        public ValueTask<IList<string>> GetUserPermisisonsAsync(AppUser user);
        public ValueTask<Permission?> FindPermissionAsync(string name);
        public ValueTask<IdentityResult> IssuePermissionsToUserAsync(AppUser user, IList<string> permissions);
        public ValueTask<IdentityResult> IssuePermissionToUserAsync(AppUser user, string permission);
        public ValueTask<IdentityResult> RemoveUserFromPermissionAsync(AppUser user, Permission permission);
        public ValueTask<IdentityResult> RemoveUserFromPermissionsAsync(AppUser user);
    }
}
