
using eShop.Domain.Entities.Admin;

namespace eShop.AuthWebApi.Queries.Admin
{
    public record FindUserByIdQuery(Guid UserId) : IRequest<Result<FindUserResponse>>;

    public class FindUserByIdQueryHandler(
        AppManager appManager,
        ILogger<FindUserByIdQueryHandler> logger,
        IMapper mapper,
        AuthDbContext context) : IRequestHandler<FindUserByIdQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByIdQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;
        private readonly AuthDbContext context = context;

        public async Task<Result<FindUserResponse>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find user with ID {0}", request.UserId);
            try
            {
                logger.LogInformation("Attempting to find user with ID {id}", request.UserId);

                var user = await appManager.UserManager.FindByIdAsync(request.UserId);

                if (user is null)
                {
                    return logger.LogErrorWithException<FindUserResponse>(new NotFoundUserByIdException(request.UserId), actionMessage);
                }

                var acccountData = mapper.Map<AccountData>(user);
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
                    AccountData = acccountData,
                    PersonalData = personalData ?? new(),
                    PermissionsData = permissionData
                };

                logger.LogInformation("Successfully found user with ID {id}", request.UserId);
                return new(response);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<FindUserResponse>(ex, actionMessage);
            }
        }
    }
}
