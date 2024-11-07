namespace eShop.AuthApi.Commands.Admin
{
    public record RemoveUserRolesCommand(RemoveUserRolesRequest Request) : IRequest<Result<RemoveUserRolesResponse>>;

    public class RemoveUserRolesCommandHandler(
        AppManager appManager,
        ILogger<RemoveUserRolesCommandHandler> logger)
        : IRequestHandler<RemoveUserRolesCommand, Result<RemoveUserRolesResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RemoveUserRolesCommandHandler> logger = logger;

        public async Task<Result<RemoveUserRolesResponse>> Handle(RemoveUserRolesCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("remove roles from user with ID {0}", request.Request.UserId);
            try
            {
                logger.LogInformation("Attempting to remove roles from user with ID {userId}. Request ID {requestID}",
                    request.Request.UserId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if (user is null)
                {
                    return logger.LogInformationWithException<RemoveUserRolesResponse>(
                        new NotFoundException($"Cannot find user with ID {request.Request.UserId}."),
                        actionMessage, request.Request.RequestId);
                }

                foreach (var role in request.Request.Roles)
                {
                    var isInRole = await appManager.UserManager.IsInRoleAsync(user, role.Name);

                    if (!isInRole)
                    {
                        return logger.LogInformationWithException<RemoveUserRolesResponse>(
                            new BadRequestException(
                                $"User with ID {request.Request.UserId} is not in role {role.Name}."),
                            actionMessage, request.Request.RequestId);
                    }

                    var result = await appManager.UserManager.RemoveFromRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        return logger.LogErrorWithException<RemoveUserRolesResponse>(
                            new FailedOperationException(
                                $"Cannot remove role from user with ID {request.Request.UserId} " +
                                $"due to server error: {result.Errors.First().Description}."),
                            actionMessage, request.Request.RequestId);
                    }
                }

                logger.LogInformation("Successfully removed roles from user with ID {userId}.Request ID {requestId}.",
                    request.Request.UserId, request.Request.RequestId);
                return new(new RemoveUserRolesResponse()
                    { Succeeded = true, Message = "Roles were successfully removed from user" });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<RemoveUserRolesResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}