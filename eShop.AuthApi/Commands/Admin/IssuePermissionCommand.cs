namespace eShop.AuthApi.Commands.Admin
{
    internal sealed record IssuePermissionCommand(IssuePermissionRequest Request)
        : IRequest<Result<IssuePermissionsResponse>>;

    internal sealed class IssuePermissionCommandHandler(
        AppManager appManager,
        AuthDbContext context) : IRequestHandler<IssuePermissionCommand, Result<IssuePermissionsResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly AuthDbContext context = context;

        public async Task<Result<IssuePermissionsResponse>> Handle(IssuePermissionCommand request,
            CancellationToken cancellationToken)
        {
            var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with ID {request.Request.UserId}."));
            }

            var permissions = new List<Permission>();

            foreach (var p in request.Request.Permissions)
            {
                var permission = await context.Permissions.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == p, cancellationToken: cancellationToken);

                if (permission is null)
                {
                    return new(new NotFoundException($"Cannot find permission {p}."));
                }

                permissions.Add(permission);
            }

            foreach (var permission in permissions)
            {
                var alreadyHasPermission = await context.UserPermissions.AsNoTracking()
                    .AnyAsync(x => x.UserId == user.Id && x.PermissionId == permission.Id,
                        cancellationToken: cancellationToken);

                if (!alreadyHasPermission)
                {
                    await context.UserPermissions.AddAsync(
                        new UserPermissions() { PermissionId = permission.Id, UserId = user.Id }, cancellationToken);
                }

                continue;
            }

            await context.SaveChangesAsync(cancellationToken);

            return new(new IssuePermissionsResponse()
                { Succeeded = true, Message = "Successfully issued permissions." });
        }
    }
}