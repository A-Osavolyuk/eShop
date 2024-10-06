

using LanguageExt.Pipes;

namespace eShop.AuthWebApi.Services.Implementation
{
    public class PermissionManager(AuthDbContext context) : IPermissionManager
    {
        private readonly AuthDbContext context = context;

        public async ValueTask<IList<string>> GetUserPermisisonsAsync(AppUser user)
        {
            var permissions = await context.UserPermissions.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();
            var result = new List<string>();

            if (!permissions.Any()) 
            {
                return result;
            }

            foreach (var permission in permissions)
            {
                var permissionName = (await context.Permissions.AsNoTracking().SingleOrDefaultAsync(x => x.Id == permission.PermissionId))!.Name;
                result.Add(permissionName);
            }

            return result;
        }

        public async ValueTask<IdentityResult> IssuePermissionsToUserAsync(AppUser user, IList<string> permissions)
        {
            if (!permissions.Any()) 
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "Cannot add permissions. Empty permission list." });
            }

            foreach (var permission in permissions) 
            {
                var permisisonId = (await context.Permissions.AsNoTracking().SingleOrDefaultAsync(x => x.Name == permission))!.Id;
                await context.UserPermissions.AddAsync(new() { UserId = user.Id, PermissionId = permisisonId });
            }

            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async ValueTask<IdentityResult> IssuePermissionToUserAsync(AppUser user, string permission)
        {
            var permisisonId = (await context.Permissions.AsNoTracking().SingleOrDefaultAsync(x => x.Name == permission))!.Id;
            await context.UserPermissions.AddAsync(new() { UserId = user.Id, PermissionId = permisisonId });
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}
