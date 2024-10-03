
using eShop.Domain.Entities.Admin;

namespace eShop.AuthWebApi.Queries.Admin
{
    public record FindUserByEmailQuery(string Email) : IRequest<Result<FindUserResponse>>;

    public class FindUserByEmailQueryHandler(
        AppManager appManager,
        ILogger<FindUserByEmailQueryHandler> logger,
        IMapper mapper,
        AuthDbContext context) : IRequestHandler<FindUserByEmailQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByEmailQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;
        private readonly AuthDbContext context = context;

        public async Task<Result<FindUserResponse>> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting to find user with email {email}", request.Email);

                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<FindUserResponse>(new NotFoundUserByEmailException(request.Email), actionMessage);
                }

                var accountData = mapper.Map<AccountData>(user);
                var personalData = await context.PersonalData.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id);
                var rolesList = await appManager.UserManager.GetRolesAsync(user);

                if (rolesList is null || !rolesList.Any())
                {
                    return logger.LogErrorWithException<FindUserResponse>(new NotFoundRolesException(), actionMessage);
                }

                var permissionData = new PermissionsData() { Id = Guid.Parse(user.Id) };
                foreach (var role in rolesList)
                {
                    var roleInfo = await appManager.RoleManager.FindByNameAsync(role);

                    if (roleInfo is null)
                    {
                        return logger.LogErrorWithException<FindUserResponse>(new NotFoundRoleException(role), actionMessage);
                    }

                    permissionData.Roles.Add(new RoleInfo()
                    {
                        Id = Guid.Parse(roleInfo.Id),
                        Name = roleInfo.Name!,
                        NormalizedName = roleInfo.NormalizedName!
                    });
                }

                var response = new FindUserResponse()
                {
                    AccountData = accountData,
                    PersonalData = personalData ?? new(),
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
