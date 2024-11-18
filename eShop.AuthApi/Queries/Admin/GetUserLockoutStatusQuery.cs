using eShop.Application.Mapping;

namespace eShop.AuthApi.Queries.Admin
{
    internal sealed record GetUserLockoutStatusQuery(string Email) : IRequest<Result<LockoutStatusResponse>>;

    internal sealed class GetUserLockoutStatusQueryHandler(
        AppManager appManager,
        ILogger<GetUserLockoutStatusQueryHandler> logger) : IRequestHandler<GetUserLockoutStatusQuery, Result<LockoutStatusResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetUserLockoutStatusQueryHandler> logger = logger;

        public async Task<Result<LockoutStatusResponse>> Handle(GetUserLockoutStatusQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("check user with email {0} lockout status", request.Email);
            try
            {
                logger.LogInformation("Attempting to check lockout status of user with email {email}.", request.Email);

                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<LockoutStatusResponse>(
                        new NotFoundException($"Cannot find user with email {request.Email}."), actionMessage);
                }

                var lockoutStatus = await appManager.UserManager.GetLockoutStatusAsync(user);

                logger.LogInformation("Successfully checked lockout status of user with email {email}.", request.Email);
                return new(UserMapper.ToUserLockoutStatusResponse(lockoutStatus));
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<LockoutStatusResponse>(ex, actionMessage);
            }
        }
    }
}
