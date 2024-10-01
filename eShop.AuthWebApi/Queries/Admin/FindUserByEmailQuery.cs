
namespace eShop.AuthWebApi.Queries.Admin
{
    public record FindUserByEmailQuery(string Email) : IRequest<Result<FindUserResponse>>;

    public class FindUserByEmailQueryHandler(
        AppManager appManager,
        ILogger<FindUserByEmailQueryHandler> logger,
        IMapper mapper) : IRequestHandler<FindUserByEmailQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByEmailQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<FindUserResponse>> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting to find user with email {email}", request.Email);

                var user = await appManager.UserManager.FindByIdAsync(request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<FindUserResponse>(new NotFoundUserByEmailException(request.Email), actionMessage);
                }

                logger.LogInformation("Successfully found user with email {email}", request.Email);
                var response = mapper.Map<FindUserResponse>(user);
                return new(response);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<FindUserResponse>(ex, actionMessage);
            }
        }
    }
}
