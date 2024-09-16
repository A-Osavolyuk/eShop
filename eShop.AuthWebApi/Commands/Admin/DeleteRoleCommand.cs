
namespace eShop.AuthWebApi.Commands.Admin
{
    public record DeleteRoleCommand(DeleteRoleRequest Request) : IRequest<Result<DeleteRoleResponse>>;

    public class DeleteRoleCommandHandler(
        AppManager appManager,
        ILogger<DeleteRoleCommandHandler> logger) : IRequestHandler<DeleteRoleCommand, Result<DeleteRoleResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<DeleteRoleCommandHandler> logger = logger;

        public async Task<Result<DeleteRoleResponse>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete role {0} with ID {1}", request.Request.Name, request.Request.Id);
            try
            {
                logger.LogInformation("Attempting to delete role {roleName} with ID {roleId}. Request ID {requestId}", 
                    request.Request.Name, request.Request.Id, request.Request.RequestId);

                var role = await appManager.RoleManager.FindByIdAsync(request.Request.Id.ToString());

                if(role is null || role.Id != request.Request.Id.ToString())
                {
                    return logger.LogErrorWithException<DeleteRoleResponse>(new NotFoundRoleException(request.Request.Id), actionMessage, request.Request.RequestId);
                }

                if(role.Name != request.Request.Name)
                {
                    return logger.LogErrorWithException<DeleteRoleResponse>(new NotFoundRoleException(request.Request.Name), actionMessage, request.Request.RequestId);
                }

                var result = await appManager.RoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteRoleResponse>(new NotDeletedRoleException(result.Errors.First()), actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Role {roleName} with ID {roleId} was successfully deleted. Request ID {requestId}", 
                    request.Request.Name, request.Request.Id, request.Request.RequestId);

                return new(new DeleteRoleResponse() { Message = "Role was successfully deleted.", Succeeded = true });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<DeleteRoleResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
