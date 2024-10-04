namespace eShop.AuthWebApi.Commands.Admin
{
    public record UnlockUserCommand(UnlockUserRequest Request) : IRequest<Result<UnlockUserResponse>>;

    public class UnlockUserCommandHandler(
        AppManager appManager,
        ILogger<UnlockUserCommandHandler> logger) : IRequestHandler<UnlockUserCommand, Result<UnlockUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<UnlockUserCommandHandler> logger = logger;

        public async Task<Result<UnlockUserResponse>> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            var actionmessage = new ActionMessage("unlock user account with ID {0}", request.Request.UserId);

            try
            {
                logger.LogInformation("Attempting to unlock user account with ID {id}. Request ID {requestId}.", request.Request.UserId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if (user is null)
                {
                    return logger.LogErrorWithException<UnlockUserResponse>(new NotFoundUserByIdException(request.Request.UserId), actionmessage, request.Request.RequestId);
                }

                var lockoutStatus = await appManager.UserManager.GetLockoutStatusAsync(user);

                if (lockoutStatus is null)
                {
                    return logger.LogErrorWithException<UnlockUserResponse>(new NoLockoutStatusException(), actionmessage, request.Request.RequestId);
                }

                if (lockoutStatus.LockoutEnabled)
                {
                    var result = await appManager.UserManager.UnlockUserAsync(user);

                    if (!result.Succeeded)
                    {
                        return logger.LogErrorWithException<UnlockUserResponse>(new NotUnlockedAccountException(), actionmessage, request.Request.RequestId);
                    }

                    logger.LogInformation("Account of user with ID {id} was successfully unlocked. Request ID {requestId}.", request.Request.UserId, request.Request.RequestId);

                    return new(new UnlockUserResponse() { Succeeded = true, Message = "User account was successfully unlocked." });
                }
                else
                {
                    logger.LogInformation("Account of user with ID {id} was not locked out. Request ID {requestId}.", request.Request.UserId, request.Request.RequestId);
                    return new(new UnlockUserResponse() { Succeeded = true, Message = "User account was not locked out." });
                }
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<UnlockUserResponse>(ex, actionmessage, request.Request.RequestId);
            }
        }
    }
}
