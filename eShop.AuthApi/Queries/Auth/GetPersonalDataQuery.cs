namespace eShop.AuthApi.Queries.Auth;

internal sealed record GetPersonalDataQuery(string Email) : IRequest<Result<PersonalDataResponse>>;

internal sealed class GetPersonalDataQueryHandler(
    AppManager appManager,
    ICacheService cacheService) : IRequestHandler<GetPersonalDataQuery, Result<PersonalDataResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly ICacheService cacheService = cacheService;

    public async Task<Result<PersonalDataResponse>> Handle(GetPersonalDataQuery request,
        CancellationToken cancellationToken)
    {
        var key = $"personal-data-{request.Email}";
        var data = await cacheService.GetAsync<PersonalDataEntity>(key);

        if (data is null)
        {
            var user = await appManager.UserManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with email {request.Email}."));
            }
            
            var personalData = await appManager.ProfileManager.FindPersonalDataAsync(user);

            if (personalData is null)
            {
                return new(new NotFoundException(
                    $"Cannot find or user with email {user.Email} has no personal data."));
            }

            await cacheService.SetAsync(key, personalData, TimeSpan.FromHours(6));
            
            return new(PersonalDataMapper.ToPersonalDataResponse(personalData));
        }

        return new (PersonalDataMapper.ToPersonalDataResponse(data));
    }
}