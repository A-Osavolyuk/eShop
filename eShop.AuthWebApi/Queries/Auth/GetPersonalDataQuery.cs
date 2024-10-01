namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetPersonalDataQuery(string Email) : IRequest<Result<PersonalDataResponse>>;

    public class GetPersonalDataQueryHandler(
        ILogger<GetPersonalDataQueryHandler> logger,
        AppManager appManager,
        IMapper mapper) : IRequestHandler<GetPersonalDataQuery, Result<PersonalDataResponse>>
    {
        private readonly ILogger<GetPersonalDataQueryHandler> logger = logger;
        private readonly AppManager appManager = appManager;
        private readonly IMapper mapper = mapper;

        public async Task<Result<PersonalDataResponse>> Handle(GetPersonalDataQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find personal data of user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting to find personal data of user with email {email}", request.Email);
                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<PersonalDataResponse>(new NotFoundUserByEmailException(request.Email), actionMessage);
                }

                logger.LogInformation("Successfully found personal data of user with email {email}", request.Email);
                return new(mapper.Map<PersonalDataResponse>(user));
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<PersonalDataResponse>(ex, actionMessage);
            }
        }
    }
}
