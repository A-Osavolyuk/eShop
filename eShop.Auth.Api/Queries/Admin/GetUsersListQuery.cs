using eShop.Domain.Types;
using UserData = eShop.Domain.Types.UserData;

namespace eShop.Auth.Api.Queries.Admin;

internal sealed record GetUsersListQuery() : IRequest<Result<IEnumerable<UserData>>>;

internal sealed class GetUsersListQueryHandler(
    AppManager appManager) : IRequestHandler<GetUsersListQuery, Result<IEnumerable<UserData>>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<IEnumerable<UserData>>> Handle(GetUsersListQuery request,
        CancellationToken cancellationToken)
    {
        var usersList = await appManager.UserManager.Users.AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        if (!usersList.Any())
        {
            return new(new List<UserData>());
        }

        var users = new List<UserData>();

        foreach (var user in usersList)
        {
            var accountData = UserMapper.ToAccountData(user);
            var personalData = await appManager.ProfileManager.FindPersonalDataAsync(user);
            var rolesList = await appManager.UserManager.GetRolesAsync(user);

            if (!rolesList.Any())
            {
                return new(new NotFoundException($"Cannot find roles for user with ID {user.Id}."));
            }

            var rolesData = (await appManager.RoleManager.GetRolesInfoAsync(rolesList) ?? Array.Empty<RoleInfo>()).ToList();
            var permissions = await appManager.PermissionManager.GetUserPermissionsAsync(user);
            var roleInfos = rolesData.ToList();

            if (!roleInfos.Any())
            {
                return new(new NotFoundException("Cannot find roles data."));
            }

            var permissionsList = new List<PermissionEntity>();

            foreach (var permission in permissions)
            {
                var permissionInfo = await appManager.PermissionManager.FindPermissionAsync(permission);

                if (permissionInfo is null)
                {
                    return new(new NotFoundException($"Cannot find permission {permission}."));
                }

                permissionsList.Add(new PermissionEntity()
                {
                    Id = permissionInfo.Id,
                    Name = permissionInfo.Name,
                });
            }

            var permissionData = new PermissionsData()
            {
                Roles = roleInfos.ToList(),
                Permissions = permissionsList
            };

            users.Add(new UserData()
            {
                PermissionsData = permissionData,
                PersonalDataEntity = personalData ?? new(),
                AccountData = accountData
            });
        }

        return users;
    }
}