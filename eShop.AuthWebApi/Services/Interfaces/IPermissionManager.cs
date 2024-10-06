namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IPermissionManager
    {
        public ValueTask<IList<string>> GetUserPermisisonsAsync(AppUser user);
        public ValueTask<IdentityResult> IssuePermissionsToUserAsync(AppUser user, IList<string> permissions);
        public ValueTask<IdentityResult> IssuePermissionToUserAsync(AppUser user, string permission);
    }
}
