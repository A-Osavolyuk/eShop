using eShop.Domain.Entities.AuthApi;
using eShop.Domain.Responses.AuthApi.Admin;

namespace eShop.AuthApi.Queries.Admin;

internal sealed record FindUserByIdQuery(Guid UserId) : IRequest<Result<FindUserResponse>>;

internal sealed class FindUserByIdQueryHandler(
    AppManager appManager,
    AuthDbContext context) : IRequestHandler<FindUserByIdQuery, Result<FindUserResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly AuthDbContext context = context;

    public async Task<Result<FindUserResponse>> Handle(FindUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with ID {request.UserId}."));
        }

        var accountData = UserMapper.ToAccountData(user);
        var personalData =
            await context.PersonalData.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);
        var rolesList = await appManager.UserManager.GetRolesAsync(user);
        var permissions = await appManager.PermissionManager.GetUserPermisisonsAsync(user);

        if (!rolesList.Any())
        {
            return new(new NotFoundException($"Cannot find roles for user with ID {user.Id}."));
        }

        var permissionData = new PermissionsData() { Id = Guid.Parse(user.Id) };

        foreach (var role in rolesList)
        {
            var roleInfo = await appManager.RoleManager.FindByNameAsync(role);

            if (roleInfo is null)
            {
                return new(new NotFoundException($"Cannot find role {role}"));
            }

            permissionData.Roles.Add(new RoleInfo()
            {
                Id = Guid.Parse(roleInfo.Id),
                Name = roleInfo.Name!,
                NormalizedName = roleInfo.NormalizedName!
            });
        }

        foreach (var permission in permissions)
        {
            var permissionInfo = await context.Permissions.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == permission, cancellationToken: cancellationToken);

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

        var response = new FindUserResponse()
        {
            AccountData = accountData,
            PersonalDataEntity = personalData ?? new(),
            PermissionsData = permissionData
        };

        return new(response);
    }
}