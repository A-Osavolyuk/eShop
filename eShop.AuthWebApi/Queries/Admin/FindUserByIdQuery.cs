
namespace eShop.AuthWebApi.Queries.Admin
{
    public record FindUserByIdQuery(Guid UserId) : IRequest<Result<FindUserResponse>>;

    public class FindUserByIdQueryHandler(
        AppManager appManager,
        ILogger<FindUserByIdQueryHandler> logger,
        IMapper mapper,
        AuthDbContext context) : IRequestHandler<FindUserByIdQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByIdQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;
        private readonly AuthDbContext context = context;

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

                var personalData = await context.PersonalData.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (personalData is null)
                {
                    var response = new FindUserResponse() with { AccountData = mapper.Map<AccountData>(user), PersonalData = new() };
                    logger.LogInformation("Successfully found user with ID {id}", request.UserId);
                    return new(response);
                }
                else
                {
                    var response = new FindUserResponse() with { AccountData = mapper.Map<AccountData>(user), PersonalData = personalData };
                    logger.LogInformation("Successfully found user with ID {id}", request.UserId);
                    return new(response);
                }
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<FindUserResponse>(ex, actionMessage);
            }
        }
    }
}
