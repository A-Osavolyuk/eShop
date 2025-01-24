﻿namespace eShop.Auth.Api.Queries.Admin;

internal sealed record FindUserByEmailQuery(string Email) : IRequest<Result<UserDto>>;

internal sealed class FindUserByEmailQueryHandler(
    AppManager appManager) : IRequestHandler<FindUserByEmailQuery, Result<UserDto>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<UserDto>> Handle(FindUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Email}."));
        }

        var accountData = UserMapper.ToAccountData(user);
        var personalData = await appManager.ProfileManager.FindPersonalDataAsync(user);
        var rolesList = await appManager.UserManager.GetRolesAsync(user);
        var permissions = await appManager.PermissionManager.GetUserPermissionsAsync(user);

        if (!rolesList.Any())
        {
            return new(new NotFoundException($"Cannot find roles for user with email {user.Email}."));
        }

        var permissionData = new PermissionsData() { Id = Guid.Parse(user.Id) };
        foreach (var role in rolesList)
        {
            var roleInfo = await appManager.RoleManager.FindByNameAsync(role);

            if (roleInfo is null)
            {
                return new(new NotFoundException($"Cannot find role {role}."));
            }

            permissionData.Roles.Add(new RoleData()
            {
                Id = Guid.Parse(roleInfo.Id),
                Name = roleInfo.Name!,
                NormalizedName = roleInfo.NormalizedName!
            });
        }

        foreach (var permission in permissions)
        {
            var permissionInfo = await appManager.PermissionManager.FindPermissionAsync(permission);

            if (permissionInfo is null)
            {
                return new(new NotFoundException($"Cannot find permission {permission}."));
            }

            permissionData.Permissions.Add(new Permission()
            {
                Id = permissionInfo.Id,
                Name = permissionInfo.Name,
            });
        }

        var response = new UserDto()
        {
            AccountData = accountData,
            PersonalDataEntity = personalData ?? new PersonalDataEntity(),
            PermissionsData = permissionData
        };

        return new(response);
    }
}