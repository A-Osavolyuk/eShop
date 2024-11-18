using eShop.Application.Mapping;
using eShop.AuthApi.Data;
using eShop.Domain.Entities.Admin;

namespace eShop.AuthApi.Queries.Admin
{
    internal sealed record FindUserByEmailQuery(string Email) : IRequest<Result<FindUserResponse>>;

    internal sealed class FindUserByEmailQueryHandler(
        AppManager appManager,
        ILogger<FindUserByEmailQueryHandler> logger,
        AuthDbContext context) : IRequestHandler<FindUserByEmailQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByEmailQueryHandler> logger = logger;
        private readonly AuthDbContext context = context;

        public async Task<Result<FindUserResponse>> Handle(FindUserByEmailQuery request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting to find user with email {email}", request.Email);

                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<FindUserResponse>(
                        new NotFoundException($"Cannot find user with email {request.Email}."), actionMessage);
                }

                var accountData = UserMapper.ToAccountData(user);
                var personalData = await context.PersonalData.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);
                var rolesList = await appManager.UserManager.GetRolesAsync(user);
                var permissions = await appManager.PermissionManager.GetUserPermisisonsAsync(user);

                if (!rolesList.Any())
                {
                    return logger.LogInformationWithException<FindUserResponse>(
                        new NotFoundException($"Cannot find roles for user with email {user.Email}."), actionMessage);
                }

                var permissionData = new PermissionsData() { Id = Guid.Parse(user.Id) };
                foreach (var role in rolesList)
                {
                    var roleInfo = await appManager.RoleManager.FindByNameAsync(role);

                    if (roleInfo is null)
                    {
                        return logger.LogInformationWithException<FindUserResponse>(
                            new NotFoundException($"Cannot find role {role}."), actionMessage);
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
                        return logger.LogInformationWithException<FindUserResponse>(
                            new NotFoundException($"Cannot find permission {permission}."), actionMessage);
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
                    PersonalDataEntity = personalData ?? new PersonalDataEntity(),
                    PermissionsData = permissionData
                };

                logger.LogInformation("Successfully found user with email {email}", request.Email);
                return new(response);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<FindUserResponse>(ex, actionMessage);
            }
        }
    }
}