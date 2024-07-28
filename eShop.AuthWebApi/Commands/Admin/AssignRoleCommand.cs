namespace eShop.AuthWebApi.Commands.Admin
{
    public record AssignRoleCommand(AssignRoleRequest Request) : IRequest<Result<AssignRoleResponse>>;
    public class AssignRoleCommandHandler(
        AppManager appManager,
        ILogger<AssignRoleCommandHandler> logger) : IRequestHandler<AssignRoleCommand, Result<AssignRoleResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<AssignRoleCommandHandler> logger = logger;

        public async Task<Result<AssignRoleResponse>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Attempting to assign role {roleName} to user with ID {userId}. Request ID {requestId}",
                    request.Request.RoleName, request.Request.UserId, request.Request.RequestId);

                var role = await appManager.RoleManager.FindByNameAsync(request.Request.RoleName);
                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId.ToString());

                if (role is not null)
                {
                    if (user is not null)
                    {
                        var result = await appManager.UserManager.AddToRoleAsync(user, role.Name!);

                        if (result.Succeeded)
                        {
                            logger.LogInformation("Successfully assigned role {roleName} to user with ID {userId}. Request ID {requestID}",
                                    request.Request.RoleName, request.Request.UserId, request.Request.RequestId);
                            return new(new AssignRoleResponse() { Succeeded = true, Message = "Role was successfully assigned" });
                        }

                        var assignException = new NotAssignRoleException(result.Errors.First().Description);
                        logger.LogError("Failed to assign role {roleName} to user with ID {userId}: {Message}. Request ID {requestId}",
                        request.Request.RoleName, request.Request.UserId, assignException.Message, request.Request.RequestId);
                        return new(assignException);
                    }

                    var userException = new NotFoundUserByIdException(request.Request.UserId);
                    logger.LogError("Failed to assign role {roleName} to user with ID {userId}: {Message}. Request ID {requestId}",
                    request.Request.RoleName, request.Request.UserId, userException.Message, request.Request.RequestId);
                    return new();
                }

                var roleException = new NotFoundRoleException(request.Request.RoleName);
                logger.LogError("Failed to assign role {roleName} to user with ID {userId}: {Message}. Request ID {requestId}",
                    request.Request.RoleName, request.Request.UserId, roleException.Message, request.Request.RequestId);
                return new(roleException);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to assign role {roleName} to user with ID {userId}: {Message}. Request ID {requestId}",
                    request.Request.RoleName, request.Request.UserId, ex.Message, request.Request.RequestId);
                return new(ex);
            }
        }
    }
}
