
namespace eShop.AuthWebApi.Queries.Admin
{
    public record FindUserByEmailQuery(string Email) : IRequest<Result<FindUserResponse>>;

    public class FindUserByEmailQueryHandler(
        AppManager appManager,
        ILogger<FindUserByEmailQueryHandler> logger,
        IMapper mapper,
        AuthDbContext context) : IRequestHandler<FindUserByEmailQuery, Result<FindUserResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<FindUserByEmailQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;
        private readonly AuthDbContext context = context;

        public async Task<Result<FindUserResponse>> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting to find user with email {email}", request.Email);

                var user = await appManager.UserManager.FindByEmailAsync(request.Email);
                
                if (user is null)
                {
                    return logger.LogErrorWithException<FindUserResponse>(new NotFoundUserByEmailException(request.Email), actionMessage);
                }

                var personalData = await context.PersonalData.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id);

                if(personalData is null)
                {
                    var response = new FindUserResponse() with { AccountData = mapper.Map<AccountData>(user), PersonalData = new() };
                    logger.LogInformation("Successfully found user with email {email}", request.Email);
                    return new(response);
                }
                else
                {
                    var response = new FindUserResponse() with { AccountData = mapper.Map<AccountData>(user), PersonalData = personalData };
                    logger.LogInformation("Successfully found user with email {email}", request.Email);
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
