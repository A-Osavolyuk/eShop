using eShop.Domain.Entities.Admin;

namespace eShop.AuthWebApi.Queries.Admin
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

        public async Task<Result<IEnumerable<UserData>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("get all users list");
            try
            {
                logger.LogInformation("Attempting to get all users list.");

                var usersList = await appManager.UserManager.Users.AsNoTracking().ToListAsync();

                if (usersList is null || !usersList.Any())
                {
                    return logger.LogErrorWithException<IEnumerable<UserData>>(new NoUsersException(), actionMessage);
                }

                var users = new List<UserData>();

                foreach (var user in usersList)
                {
                    var accountData = mapper.Map<AccountData>(user);
                    var personalData = await context.PersonalData.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id);
                    var rolesList = await appManager.UserManager.GetRolesAsync(user);

                    if (rolesList is null || !rolesList.Any())
                    {
                        return logger.LogErrorWithException<IEnumerable<UserData>>(new NotFoundRolesException(), actionMessage);
                    }

                    var rolesData = await appManager.RoleManager.GetRolesInfoAsync(rolesList);
                    var permissions = await appManager.PermissionManager.GetUserPermisisonsAsync(user);

                    if (rolesData is null || !rolesData.Any())
                    {
                        return logger.LogErrorWithException<IEnumerable<UserData>>(new NoRoleInfoException(), actionMessage);
                    }

                    var permissionsList = new List<Permission>();

                    foreach (var permission in permissions)
                    {
                        var permissionInfo = await context.Permissions.AsNoTracking().SingleOrDefaultAsync(x => x.Name == permission);

                        if (permissionInfo is null)
                        {
                            return logger.LogErrorWithException<IEnumerable<UserData>>(new NotFoundPermissison(permission), actionMessage);
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
