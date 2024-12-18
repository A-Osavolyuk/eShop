using eShop.Domain.Entities.AuthApi;

namespace eShop.AuthApi.Queries.Admin;

internal sealed record GetPermissionsListQuery() : IRequest<Result<IEnumerable<Permission>>>;

internal sealed class GetPermissionsListQueryHandler(
    AppManager appManager) : IRequestHandler<GetPermissionsListQuery, Result<IEnumerable<Permission>>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<IEnumerable<Permission>>> Handle(GetPermissionsListQuery request,
        CancellationToken cancellationToken)
    {
        var permissions = await appManager.PermissionManager.GetPermissionsAsync();

        if (!permissions.Any())
        {
            return new(new NotFoundException("Cannot find permissions."));
        }

        return permissions.ToList();
    }
}