
using eShop.Domain.Entities;

namespace eShop.AuthWebApi.Commands.Admin
{
    public record RemoveUserRolesCommand(RemoveUserRolesRequest RemoveUserRoles) : IRequest<Result<RemoveUserRolesResponse>>;

    public class RemoveUserRolesCommandHandler(
        AppManager appManager,
        ILogger<RemoveUserRolesCommandHandler> logger) : IRequestHandler<RemoveUserRolesCommand, Result<RemoveUserRolesResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RemoveUserRolesCommandHandler> logger = logger;

        public async Task<Result<RemoveUserRolesResponse>> Handle(RemoveUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Attempting to remove roles from user with ID {userId}. Request ID {requestID}", 
                    request.RemoveUserRoles.UserId, request.RemoveUserRoles.RequestId);
                var user = await appManager.UserManager.FindByIdAsync(request.RemoveUserRoles.UserId);

                if (user is not null)
                {
                    foreach(var role in request.RemoveUserRoles.Roles)
                    {
                        var isInRole = await appManager.UserManager.IsInRoleAsync(user, role.Name);

                        if (isInRole)
                        {
                            var result = await appManager.UserManager.RemoveFromRoleAsync(user, role.Name);

                            if (!result.Succeeded)
                            {
                                var notRemovedRoleException = new NotRemovedRoleException(result.Errors.First().Description);
                                logger.LogError("Failed to remove roles from user with ID {userId}: {message}. Request ID {requestId}",
                                    request.RemoveUserRoles.UserId, notRemovedRoleException.Message, request.RemoveUserRoles.RequestId);
                                return new(notRemovedRoleException);
                            }
                        }
                        else
                        {
                            var userNotInRoleException = new UserNotInRoleException(role.Name);
                            logger.LogError("Failed to remove roles from user with ID {userId}: {message}. Request ID {requestId}",
                                request.RemoveUserRoles.UserId, userNotInRoleException.Message, request.RemoveUserRoles.RequestId);
                            return new(userNotInRoleException);
                        }
                    }

                    logger.LogInformation("Successfully removed roles from user with ID {userId}.Request ID {requestId}.", 
                        request.RemoveUserRoles.UserId, request.RemoveUserRoles.RequestId);
                    return new(new RemoveUserRolesResponse() { Succeeded = true, Message = "Roles were successfully removed from user" });
                }
                var notFoundUserException = new NotFoundUserByIdException(request.RemoveUserRoles.RequestId);
                logger.LogError("Failed to remove roles from user with ID {userId}: {message}. Request ID {requestId}",
                    request.RemoveUserRoles.UserId, notFoundUserException.Message, request.RemoveUserRoles.RequestId);
                return new(notFoundUserException);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to remove roles from user with ID {userId}: {message}. Request ID {requestId}", 
                    request.RemoveUserRoles.UserId, ex.Message, request.RemoveUserRoles.RequestId);
                return new(ex);
            }
        }
    }
}
