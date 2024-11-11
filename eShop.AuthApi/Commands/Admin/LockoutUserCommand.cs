
namespace eShop.AuthApi.Commands.Admin
{
    internal sealed record LockoutUserCommand(LockoutUserRequest Request) : IRequest<Result<LockoutUserResponse>>;

    internal sealed class LockoutUserCommandHandler(
        AppManager appManager,
        ILogger<LockoutUserCommandHandler> logger) : IRequestHandler<LockoutUserCommand, Result<LockoutUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<LockoutUserCommandHandler> logger = logger;

        public async Task<Result<LockoutUserResponse>> Handle(LockoutUserCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("lockout user with ID {0}", request.Request.UserId);
            try
            {
                logger.LogInformation("Attempting to lockout user with ID {id}. Request ID {requestID}.", request.Request.RequestId, request.Request.RequestId);

                var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

                if (user is null)
                {
                    return logger.LogInformationWithException<LockoutUserResponse>(
                        new NotFoundException($"Cannot find user with ID {request.Request.UserId}."), 
                        actionMessage, request.Request.UserId);
                }

                if (request.Request.Permanent)
                {
                    var lockoutEndDate = DateTime.UtcNow.AddYears(100);
                    await appManager.UserManager.SetLockoutEnabledAsync(user, true);
                    await appManager.UserManager.SetLockoutEndDateAsync(user, lockoutEndDate);

                    logger.LogInformation("User with ID {id} was successfully permanently banned. Request ID {requestID}.", 
                        request.Request.UserId, request.Request.RequestId);
                    return new(new LockoutUserResponse()
                    {
                        LockoutEnabled = true,
                        LockoutEnd = lockoutEndDate,
                        Message = "User was successfully permanently banned.",
                        Succeeded = true
                    });
                }
                else
                {
                    await appManager.UserManager.SetLockoutEnabledAsync(user, true);
                    await appManager.UserManager.SetLockoutEndDateAsync(user, request.Request.LockoutEnd);

                    logger.LogInformation("User with ID {id} was successfully banned until {lockoutEndDate}. Request ID {requestID}.", 
                        request.Request.UserId, request.Request.LockoutEnd, request.Request.RequestId);
                    return new(new LockoutUserResponse()
                    {
                        LockoutEnabled = true,
                        LockoutEnd = request.Request.LockoutEnd,
                        Message = $"User was successfully banned until {request.Request.LockoutEnd}.",
                        Succeeded = true
                    });
                }
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<LockoutUserResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
