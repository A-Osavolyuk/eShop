namespace eShop.AuthApi.Queries.Admin
{
    internal sealed record GetUserRolesQuery(Guid Id) : IRequest<Result<UserRolesResponse>>;

    internal sealed class GetUserRolesQueryHandler(
        AppManager appManager,
        ILogger<GetUserRolesQueryHandler> logger) : IRequestHandler<GetUserRolesQuery, Result<UserRolesResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetUserRolesQueryHandler> logger = logger;

        public async Task<Result<UserRolesResponse>> Handle(GetUserRolesQuery request,
            CancellationToken cancellationToken)
        {
            var user = await appManager.UserManager.FindByIdAsync(request.Id);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with ID {request.Id}."));
            }

            var roleList = await appManager.UserManager.GetRolesAsync(user);

            if (!roleList.Any())
            {
                return new(new NotFoundException($"Cannot find roles for user with ID {request.Id}."));
            }

            var result = new UserRolesResponse() with { UserId = Guid.Parse(user.Id) };

            foreach (var role in roleList)
            {
                var roleInfo = await appManager.RoleManager.FindByNameAsync(role);

                if (roleInfo is null)
                {
                    return new(new NotFoundException($"Cannot find role {role}"));
                }

                result.Roles.Add(new RoleInfo()
                {
                    Id = Guid.Parse(roleInfo.Id),
                    Name = roleInfo.Name!,
                    NormalizedName = roleInfo.NormalizedName!
                });
            }

            logger.LogInformation("Successfully got roles of user with ID {id}.", request.Id);
            return result;
        }
    }
}