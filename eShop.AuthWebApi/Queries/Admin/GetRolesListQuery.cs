namespace eShop.AuthWebApi.Queries.Admin
{
    public record GetRolesListQuery() : IRequest<Result<IEnumerable<RoleDto>>>;

    public class GetRolesListQueryHandler(
        AppManager appManager,
        ILogger<GetRolesListQueryHandler> logger) : IRequestHandler<GetRolesListQuery, Result<IEnumerable<RoleDto>>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetRolesListQueryHandler> logger = logger;

        public async Task<Result<IEnumerable<RoleDto>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("get list of roles");
            try
            {
                logger.LogInformation("Attempting to get list of roles.");

                var roles = await appManager.RoleManager.Roles.ToListAsync(cancellationToken);

                if (roles.Count == 0)
                {
                    return logger.LogErrorWithException<IEnumerable<RoleDto>>(new NotFoundRolesException(), actionMessage);
                }

                var response = new List<RoleDto>();
                foreach (var role in roles)
                {
                    if (role is null)
                    {
                        return logger.LogErrorWithException<IEnumerable<RoleDto>>(new NullableRoleException(), actionMessage);
                    }

                    var memberCount = await appManager.RoleManager.Roles
                        .Where(x => x.Id == role.Id)
                        .CountAsync();

                    response.Add(new()
                    {
                        Id = Guid.Parse(role!.Id),
                        Name = role.Name!,
                        NormalizedName = role.NormalizedName!,
                        MembersCount = memberCount,
                    });
                }

                logger.LogInformation("Successfully got list of roles.");
                return new(response);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<RoleDto>>(ex, actionMessage);
            }
        }
    }
}
