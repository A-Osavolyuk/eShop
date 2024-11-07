using eShop.AuthApi.Data;
using eShop.Domain.Entities.Admin;

namespace eShop.AuthApi.Queries.Admin
{
    public record GetUsersListQuery() : IRequest<Result<IEnumerable<UserData>>>;

    public class GetUsersListQueryHandler(
        AppManager appManager,
        ILogger<GetUsersListQueryHandler> logger,
        AuthDbContext context,
        IMapper mapper) : IRequestHandler<GetUsersListQuery, Result<IEnumerable<UserData>>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetUsersListQueryHandler> logger = logger;
        private readonly AuthDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<UserData>>> Handle(GetUsersListQuery request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("get all users list");
            try
            {
                logger.LogInformation("Attempting to get all users list.");

                var usersList = await appManager.UserManager.Users.AsNoTracking().ToListAsync();

                if (usersList is null)
                {
                    return logger.LogInformationWithException<IEnumerable<UserData>>(
                        new NotFoundException("Cannot find any users."), actionMessage);
                }

                if (!usersList.Any())
                {
                    logger.LogInformation("Successfully got list of all users.");
                    return new(new List<UserData>());
                }

                var users = new List<UserData>();

                foreach (var user in usersList)
                {
                    var accountData = mapper.Map<AccountData>(user);
                    var personalData = await context.PersonalData.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);
                    var rolesList = await appManager.UserManager.GetRolesAsync(user);

                    if (rolesList is null || !rolesList.Any())
                    {
                        return logger.LogInformationWithException<IEnumerable<UserData>>(
                            new NotFoundException($"Cannot find roles for user with ID {user.Id}."), actionMessage);
                    }

                    var rolesData = await appManager.RoleManager.GetRolesInfoAsync(rolesList);
                    var permissions = await appManager.PermissionManager.GetUserPermisisonsAsync(user);

                    if (rolesData is null || !rolesData.Any())
                    {
                        return logger.LogInformationWithException<IEnumerable<UserData>>(
                            new NotFoundException("Cannot find roles data."), actionMessage);
                    }

                    var permissionsList = new List<Permission>();

                    foreach (var permission in permissions)
                    {
                        var permissionInfo = await context.Permissions.AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Name == permission, cancellationToken: cancellationToken);

                        if (permissionInfo is null)
                        {
                            return logger.LogInformationWithException<IEnumerable<UserData>>(
                                new NotFoundException($"Cannot find permission {permission}."), actionMessage);
                        }

                        permissionsList.Add(new Permission()
                        {
                            Id = permissionInfo.Id,
                            Name = permissionInfo.Name,
                        });
                    }

                    var permissionData = new PermissionsData()
                    {
                        Roles = rolesData.ToList(),
                        Permissions = permissionsList
                    };

                    users.Add(new UserData()
                    {
                        PermissionsData = permissionData,
                        PersonalData = personalData ?? new(),
                        AccountData = accountData
                    });
                }

                logger.LogInformation("Successfully got list of all users.");
                return users;
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<UserData>>(ex, actionMessage);
            }
        }
    }
}