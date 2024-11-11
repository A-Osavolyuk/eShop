using eShop.Domain.Entities.Admin;

namespace eShop.AuthApi.Queries.Admin
{
    internal sealed record GetPermissionsListQuery() : IRequest<Result<IEnumerable<Permission>>>;

    internal sealed class GetPermissionsListQueryHandler(
        AppManager appManager,
        ILogger<GetPermissionsListQueryHandler> logger) : IRequestHandler<GetPermissionsListQuery, Result<IEnumerable<Permission>>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetPermissionsListQueryHandler> logger = logger;

        public async Task<Result<IEnumerable<Permission>>> Handle(GetPermissionsListQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("get list of permissions");
            try
            {
                logger.LogInformation("Attempting to get lost of permissions.");

                var permissions = await appManager.PermissionManager.GetPermissionsAsync();

                if (!permissions.Any()) 
                {
                    return logger.LogInformationWithException<IEnumerable<Permission>>(
                        new NotFoundException("Cannot find permissions."), actionMessage);
                }

                return permissions.ToList();
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<Permission>>(ex, actionMessage);
            }
        }
    }
}
