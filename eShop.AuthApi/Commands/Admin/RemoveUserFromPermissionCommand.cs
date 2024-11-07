namespace eShop.AuthApi.Commands.Admin
{
    public record RemoveUserFromPermissionCommand(RemoveUserFromPermissionRequest Request)
        : IRequest<Result<RemoveUserFromPermissionResponse>>;

    public class RemoveUserFromPermissionCommandHandler(
        AppManager appManager,
        ILogger<RemoveUserFromPermissionCommandHandler> logger)
        : IRequestHandler<RemoveUserFromPermissionCommand, Result<RemoveUserFromPermissionResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RemoveUserFromPermissionCommandHandler> logger = logger;

        public async Task<Result<RemoveUserFromPermissionResponse>> Handle(RemoveUserFromPermissionCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("remove user with ID {0} from permission {1}", request.Request.UserId,
                request.Request.RequestId);
            try
            {
                logger.LogInformation(
                    "Attempting to remove user with ID {id} from permission {name}. Request ID {requestID}",
                    request.Request.UserId, request.Request.PermissionName, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if (user is null)
                {
                    return logger.LogInformationWithException<RemoveUserFromPermissionResponse>(
                        new NotFoundException($"Cannot find user with ID {request.Request.UserId}."),
                        actionMessage, request.Request.RequestId);
                }

                var permission = await appManager.PermissionManager.FindPermissionAsync(request.Request.PermissionName);

                if (permission is null)
                {
                    return logger.LogInformationWithException<RemoveUserFromPermissionResponse>(
                        new NotFoundException($"Cannot find permission {request.Request.PermissionName}."),
                        actionMessage, request.Request.RequestId);
                }

                var hasUserPermission =
                    await appManager.PermissionManager.UserHasPermissionAsync(user, permission.Name);

                if (!hasUserPermission)
                {
                    logger.LogInformation("User with ID {id} does not have permisison {name}. Reqeust ID {requestId}.",
                        request.Request.UserId, request.Request.PermissionName, request.Request.RequestId);
                    return new(new RemoveUserFromPermissionResponse()
                        { Succeeded = true, Message = "User does not have the permission." });
                }
                else
                {
                    var permissionResult =
                        await appManager.PermissionManager.RemoveUserFromPermissionAsync(user, permission);

                    if (!permissionResult.Succeeded)
                    {
                        return logger.LogErrorWithException<RemoveUserFromPermissionResponse>(
                            new FailedOperationException($"Cannot remove user from permission {permission.Name} " +
                                                         $"due to server error: {permissionResult.Errors.First().Description}."),
                            actionMessage, request.Request.RequestId);
                    }

                    logger.LogInformation(
                        "Successfuly removed permission {from} user with ID {id}. Reqeust ID {requestId}.",
                        request.Request.PermissionName, request.Request.UserId, request.Request.RequestId);

                    return new(new RemoveUserFromPermissionResponse()
                        { Succeeded = true, Message = "Successfully removed user form permission." });
                }
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<RemoveUserFromPermissionResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}