using eShop.Domain.Entities.AuthApi;

namespace eShop.AuthApi.Services.Implementation;

internal sealed class PermissionManager(AuthDbContext context) : IPermissionManager
{
    private readonly AuthDbContext context = context;

    public async ValueTask<Permission?> FindPermissionAsync(string name)
    {
        var permission = await context.Permissions.AsNoTracking().SingleOrDefaultAsync(x => x.Name == name);

        if (permission is null)
        {
            return default;
        }
        return permission;
    }

    public async ValueTask<IList<Permission>> GetPermissionsAsync()
    {
        var permissions = await context.Permissions.AsNoTracking().ToListAsync();
        return permissions;
    }

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

    public async ValueTask<IdentityResult> RemoveUserFromPermissionAsync(AppUser user, Permission permission)
    {
        var userPermission = await context.UserPermissions.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == user.Id && x.PermissionId == permission.Id);

        if (userPermission is null)
        {
            return IdentityResult.Failed(
                new IdentityError() { Code = "404", Description = string.Format("Cannot find permission {0} for user with ID {1}", permission.Name, user.Id) });
        }

        context.UserPermissions.Remove(userPermission);
        await context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> RemoveUserFromPermissionsAsync(AppUser user)
    {
        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "User is null." });
        }

        var userPermissions = await context.UserPermissions.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();

        if (userPermissions.Any())
        {
            context.UserPermissions.RemoveRange(userPermissions);
            await context.SaveChangesAsync();
        }
            
        return IdentityResult.Success;
    }

    public async ValueTask<bool> UserHasPermissionAsync(AppUser user, string name)
    {
        var permission = await context.Permissions.AsNoTracking().SingleOrDefaultAsync(x => x.Name == name);

        if (permission is null)
        {
            return false;
        }

        var hasUserPermission = await context.UserPermissions.AsNoTracking().AnyAsync(x => x.UserId == user.Id && x.PermissionId == permission.Id);

        if (hasUserPermission)
        {
            return true;
        }
        return false;
    }
}