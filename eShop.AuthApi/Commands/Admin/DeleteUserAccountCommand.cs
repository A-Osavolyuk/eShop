using eShop.AuthApi.Data;

namespace eShop.AuthApi.Commands.Admin
{
    public record DeleteUserAccountCommand(DeleteUserAccountRequest Request)
        : IRequest<Result<DeleteUserAccountResponse>>;

    public class DeleteUserAccountCommandHandler(
        AppManager appManager,
        ILogger<DeleteUserAccountCommandHandler> logger,
        AuthDbContext context) : IRequestHandler<DeleteUserAccountCommand, Result<DeleteUserAccountResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<DeleteUserAccountCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;

        public async Task<Result<DeleteUserAccountResponse>> Handle(DeleteUserAccountCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete user account with ID {0}", request.Request.UserId);
            try
            {
                logger.LogInformation("Attempting to delete user account with ID {id}. Request ID {requestId}",
                    request.Request.UserId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if (user is null)
                {
                    return logger.LogInformationWithException<DeleteUserAccountResponse>(
                        new NotFoundException($"Cannot find user with ID {request.Request.UserId}"),
                        actionMessage, request.Request.RequestId);
                }

                var rolesResult = await appManager.UserManager.RemoveFromRolesAsync(user);

                if (!rolesResult.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(
                        new FailedOperationException(
                            $"Cannot remove roles from user with ID {request.Request.UserId} " +
                            $"due to server error: {rolesResult.Errors.First().Description}"),
                        actionMessage, request.Request.RequestId);
                }

                var permissionsResult = await appManager.PermissionManager.RemoveUserFromPermissionsAsync(user);

                if (!permissionsResult.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(
                        new FailedOperationException(
                            $"Cannot remove permissions from user with ID {request.Request.UserId} " +
                            $"due to server error: {permissionsResult.Errors.First().Description}"),
                        actionMessage, request.Request.RequestId);
                }

                var personalData =
                    await context.PersonalData.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == user.Id,
                        cancellationToken: cancellationToken);

                if (personalData is not null)
                {
                    context.PersonalData.Remove(personalData);
                    await context.SaveChangesAsync(cancellationToken);
                }

                var userTokens = await context.UserAuthenticationTokens.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

                if (userTokens is not null)
                {
                    context.UserAuthenticationTokens.Remove(userTokens);
                    await context.SaveChangesAsync(cancellationToken);
                }

                var accountResult = await appManager.UserManager.DeleteAsync(user);

                if (!accountResult.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(
                        new FailedOperationException($"Cannot delete user account with ID {request.Request.UserId} " +
                                                     $"due to server error: {accountResult.Errors.First().Description}"),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully deleted user account with ID {id}. Request ID {requestId}",
                    request.Request.UserId, request.Request.RequestId);

                return new(
                    new DeleteUserAccountResponse()
                    {
                        Message = "User account was successfully deleted.", Succeeded = true
                    });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<DeleteUserAccountResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}