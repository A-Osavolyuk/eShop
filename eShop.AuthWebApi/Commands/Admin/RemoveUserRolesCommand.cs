
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
                                return new(new NotRemovedRoleException(result.Errors.First().Description));
                            }
                        }
                        else
                        {
                            return new(new UserNotInRoleException(role.Name));
                        }
                    }

                    return new(new RemoveUserRolesResponse() { Succeeded = true, Message = "Roles were successfully removed from user" });
                }

                return new(new NotFoundUserByIdException(request.RemoveUserRoles.RequestId));
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to remove roles of user with ID {userId}: {message}. Request ID {requestId}", 
                    request.RemoveUserRoles.UserId, ex.Message, request.RemoveUserRoles.RequestId);
                return new(ex);
            }
        }
    }
}
