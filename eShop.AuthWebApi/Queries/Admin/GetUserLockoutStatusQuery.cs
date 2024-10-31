namespace eShop.AuthWebApi.Queries.Admin
{
    public record GetUserLockoutStatusQuery(string Email) : IRequest<Result<UserLockoutStatusResponse>>;

    public class GetUserLockoutStatusQueryHandler(
        AppManager appManager,
        ILogger<GetUserLockoutStatusQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetUserLockoutStatusQuery, Result<UserLockoutStatusResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetUserLockoutStatusQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<UserLockoutStatusResponse>> Handle(GetUserLockoutStatusQuery request, CancellationToken cancellationToken)
        {
            var actionMessagne = new ActionMessage("check user with email {0} lockout status", request.Email);
            try
            {
                logger.LogInformation("Attempting to check lockout status of user with email {email}.", request.Email);

                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<UserLockoutStatusResponse>(
                        new NotFoundException($"Cannot find user with email {request.Email}."), actionMessagne);
                }

                var lockoutStatus = await appManager.UserManager.GetLockoutStatusAsync(user);

                logger.LogInformation("Successfully checked lockout status of user with email {email}.", request.Email);
                return new(mapper.Map<UserLockoutStatusResponse>(lockoutStatus));
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<UserLockoutStatusResponse>(ex, actionMessagne);
            }
        }
    }
}
