
namespace eShop.AuthWebApi.Commands.Admin
{
    public record DeleteUserAccountCommand(DeleteUserAccountRequest Request) : IRequest<Result<DeleteUserAccountResponse>>;

    public class DeleteUserAccountCommandHandler(
        AppManager appManager,
        ILogger<DeleteUserAccountCommandHandler> logger,
        AuthDbContext context) : IRequestHandler<DeleteUserAccountCommand, Result<DeleteUserAccountResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<DeleteUserAccountCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;

        public async Task<Result<DeleteUserAccountResponse>> Handle(DeleteUserAccountCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete user account with ID {0}", request.Request.UserId);
            try
            {
                logger.LogInformation("Atttempting to delete user account with ID {id}. Request ID {requestId}", request.Request.UserId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if(user is null)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(new NotFoundUserByIdException(request.Request.RequestId), actionMessage, request.Request.RequestId);
                }

                var rolesResult = await appManager.UserManager.RemoveFromRolesAsync(user);

                if (!rolesResult.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(new NotRemovedRoleException(rolesResult.Errors.First().Description), 
                        actionMessage, request.Request.RequestId);
                }

                var permisisonsResult = await appManager.PermissionManager.RemoveUserFromPermissionsAsync(user);

                if (!permisisonsResult.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(new NotRemovedPermissionException(rolesResult.Errors),
                        actionMessage, request.Request.RequestId);
                }

                var personalData = await context.PersonalData.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == user.Id);

                if (personalData is not null) 
                { 
                    context.PersonalData.Remove(personalData);
                    await context.SaveChangesAsync();
                }

                var userTokens = await context.UserAuthenticationTokens.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == user.Id);

                if (userTokens is not null)
                {
                    context.UserAuthenticationTokens.Remove(userTokens);
                    await context.SaveChangesAsync();
                }

                var accountResult = await appManager.UserManager.DeleteAsync(user);

                if (!accountResult.Succeeded)
                {
                    return logger.LogErrorWithException<DeleteUserAccountResponse>(new NotDeletedUserAccountException(accountResult.Errors), actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully deleted user account with ID {id}. Request ID {requestId}", request.Request.UserId, request.Request.RequestId);
                return new(new DeleteUserAccountResponse() { Message = "User account was successfully deleted.", Succeeded = true });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<DeleteUserAccountResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
