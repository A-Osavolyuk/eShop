
using eShop.Application.Extensions;
using eShop.Domain.Entities;
using eShop.Domain.Responses.Admin;

namespace eShop.AuthWebApi.Commands.Admin
{
    public record RemoveUserRoleCommand(RemoveUserRoleRequest Request) : IRequest<Result<RemoveUserRoleResponse>>;

    public class RemoveUserRoleCommandHandler(
        AuthDbContext context,
        ILogger<RemoveUserRoleCommandHandler> logger,
        AppManager appManager) : IRequestHandler<RemoveUserRoleCommand, Result<RemoveUserRoleResponse>>
    {
        private readonly AuthDbContext context = context;
        private readonly ILogger<RemoveUserRoleCommandHandler> logger = logger;
        private readonly AppManager appManager = appManager;

        public async Task<Result<RemoveUserRoleResponse>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("remove role from user with ID {0}", request.Request.UserId);
            try
            {
                logger.LogInformation("Attempting to remove user role with name {roleName} from user with ID {userId}. Request ID {requestId}",
                    request.Request.Role.Name, request.Request.UserId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if(user is null)
                {
                    return logger.LogErrorWithException<RemoveUserRoleResponse>(new NotFoundUserByIdException(request.Request.UserId), actionMessage);
                }

                var isInRole = await appManager.UserManager.IsInRoleAsync(user, request.Request.Role.Name);

                if (!isInRole)
                {
                    return logger.LogErrorWithException<RemoveUserRoleResponse>(new UserNotInRoleException(request.Request.Role.Name), actionMessage, request.Request.RequestId);
                }

                var result = await appManager.UserManager.RemoveFromRoleAsync(user, request.Request.Role.Name);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<RemoveUserRoleResponse>(new NotRemovedRoleException(result.Errors.First().Description),
                                    actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully removed user with ID {userID} from role {roleName}. Request ID {requestID}", 
                    request.Request.UserId, request.Request.Role.Name, request.Request.RequestId);
                return new(new RemoveUserRoleResponse() { Succeeded = true, Message = "User was successfully removed from role." });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<RemoveUserRoleResponse>(ex, actionMessage);
            }
        }
    }
}
