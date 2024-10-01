
namespace eShop.AuthWebApi.Queries.Admin
{
    public record FindUserByIdQuery(Guid UserId) : IRequest<Result<FindUserResponse>>;

    public class FindUserByIdQueryHandler(
        AppManager appManager,
        ILogger<FindUserByIdQueryHandler> logger,
        IMapper mapper) : IRequestHandler<FindUserByIdQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByIdQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<FindUserResponse>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find user with ID {0}", request.UserId);
            try
            {
                logger.LogInformation("Attempting to find user with ID {id}", request.UserId);

                var user = await appManager.UserManager.FindByIdAsync(request.UserId);

                if (user is null)
                {
                    return logger.LogErrorWithException<FindUserResponse>(new NotFoundUserByIdException(request.UserId), actionMessage);
                }

                logger.LogInformation("Successfully found user with ID {id}", request.UserId);
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
