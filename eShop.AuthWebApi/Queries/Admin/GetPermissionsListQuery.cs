using eShop.Domain.Entities.Admin;

namespace eShop.AuthWebApi.Queries.Admin
{
    public record GetPermissionsListQuery() : IRequest<Result<IEnumerable<Permission>>>;

    public class GetPermissionsListQueryHandler(
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
                    return logger.LogErrorWithException<IEnumerable<Permission>>(new NoPermissionsException(), actionMessage);
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
