
using eShop.AuthApi.Data;
using eShop.Domain.Entities.Admin;

namespace eShop.AuthApi.Commands.Admin
{
    public record IssuePermissionCommand(IssuePermissionRequest Request) : IRequest<Result<IssuePermissionsResponse>>;

    public class IssuePermissionCommandHandler(
        AppManager appManager,
        ILogger<IssuePermissionCommandHandler> logger,
        AuthDbContext context) : IRequestHandler<IssuePermissionCommand, Result<IssuePermissionsResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<IssuePermissionCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;

        public async Task<Result<IssuePermissionsResponse>> Handle(IssuePermissionCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("issue permission to user with ID {0}", request.Request.UserId);
            try
            {
                logger.LogInformation("Attempting to issue permissions to user with ID {id}. Request ID {requestId}.", request.Request.UserId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if(user is null)
                {
                    return logger.LogInformationWithException<IssuePermissionsResponse>(
                        new NotFoundException($"Cannot find user with ID {request.Request.UserId}."), 
                        actionMessage, request.Request.RequestId);
                }

                var permissions = new List<Permission>();

                foreach (var p in request.Request.Permissions) 
                {
                    var permission = await context.Permissions.AsNoTracking().FirstOrDefaultAsync(x => x.Name == p, cancellationToken: cancellationToken);

                    if(permission is null)
                    {
                        return logger.LogInformationWithException<IssuePermissionsResponse>(
                            new NotFoundException($"Cannot find permission {p}."), 
                            actionMessage, request.Request.RequestId);
                    }

                    permissions.Add(permission);
                }

                foreach(var permission in permissions)
                {
                    var alreadyHasPermission = await context.UserPermissions.AsNoTracking().
                        AnyAsync(x => x.UserId == user.Id && x.PermissionId == permission.Id, cancellationToken: cancellationToken);

                    if (!alreadyHasPermission)
                    {
                        await context.UserPermissions.AddAsync(new UserPermissions() { PermissionId = permission.Id, UserId = user.Id }, cancellationToken);
                    }

                    continue;
                }

                await context.SaveChangesAsync(cancellationToken);

                logger.LogInformation("Successfully issued permissions for user with ID {id}. Request ID {requestId}.", request.Request.UserId, request.Request.RequestId);
                return new(new IssuePermissionsResponse() { Succeeded = true, Message = "Successfully issued permissions." });
            }
            catch (Exception ex)    
            {
                return logger.LogErrorWithException<IssuePermissionsResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
