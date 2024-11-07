namespace eShop.AuthApi.Commands.Admin
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
            var actionMessage = new ActionMessage("assigned role {0} to user with ID {1}", request.Request.RoleName, request.Request.UserId);
            try
            {
                logger.LogInformation("Attempting to assign role {roleName} to user with ID {userId}. Request ID {requestId}",
                    request.Request.RoleName, request.Request.UserId, request.Request.RequestId);

                var role = await appManager.RoleManager.FindByNameAsync(request.Request.RoleName);
                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId.ToString());

                if (role is null)
                {
                    return logger.LogInformationWithException<AssignRoleResponse>(new NotFoundException($"Cannot find role with name {request.Request.RoleName}.")
                        ,actionMessage, request.Request.RequestId);
                }

                if (user is null)
                {
                    return logger.LogInformationWithException<AssignRoleResponse>(new NotFoundException($"Cannot find user with ID {request.Request.UserId}."), 
                        actionMessage, request.Request.RequestId);
                }

                var result = await appManager.UserManager.AddToRoleAsync(user, role.Name!);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<AssignRoleResponse>(
                        new FailedOperationException($"Cannot assign role due to server error: {result.Errors.First().Description}"), 
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully assigned role {roleName} to user with ID {userId}. Request ID {requestID}",
                            request.Request.RoleName, request.Request.UserId, request.Request.RequestId);
                
                return new(new AssignRoleResponse() { Succeeded = true, Message = "Role was successfully assigned" });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<AssignRoleResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
