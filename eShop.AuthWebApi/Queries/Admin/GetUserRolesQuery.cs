
namespace eShop.AuthWebApi.Queries.Admin
{
    public record GetUserRolesQuery(Guid Id) : IRequest<Result<UserRolesResponse>>;

    public class GetUserRolesQueryHandler(
        AppManager appManager,
        ILogger<GetUserRolesQueryHandler> logger) : IRequestHandler<GetUserRolesQuery, Result<UserRolesResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetUserRolesQueryHandler> logger = logger;

        public async Task<Result<UserRolesResponse>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("get roles of user with ID {0}", request.Id);
            try
            {
                logger.LogInformation("Attempting to get roles of user with ID {id}.", request.Id);

                var user = await appManager.UserManager.FindByIdAsync(request.Id);

                if (user is null)
                {
                    return logger.LogErrorWithException<UserRolesResponse>(new NotFoundUserByIdException(request.Id), actionMessage);
                }

                var roleList = await appManager.UserManager.GetRolesAsync(user);

                if (roleList is null || !roleList.Any())
                {
                    return logger.LogErrorWithException<UserRolesResponse>(new NotFoundRolesException(), actionMessage);
                }

                var result = new UserRolesResponse() with { UserId = Guid.Parse(user.Id) };

                foreach (var role in roleList)
                {
                    var roleInfo = await appManager.RoleManager.FindByNameAsync(role);

                    if (roleInfo is null)
                    {
                        return logger.LogErrorWithException<UserRolesResponse>(new NotFoundRoleException(role), actionMessage);
                    }

                    result.Roles.Add(new RoleInfo()
                    {
                        Id = Guid.Parse(roleInfo.Id),
                        Name = roleInfo.Name!,
                        NormalizedName = roleInfo.NormalizedName!
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<UserRolesResponse>(ex, actionMessage);
            }
        }
    }
}
