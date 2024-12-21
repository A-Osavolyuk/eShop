namespace eShop.AuthApi.Queries.Auth;

internal sealed record GetPersonalDataQuery(string Email) : IRequest<Result<PersonalDataResponse>>;

internal sealed class GetPersonalDataQueryHandler(
    AppManager appManager,
    AuthDbContext context) : IRequestHandler<GetPersonalDataQuery, Result<PersonalDataResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly AuthDbContext context = context;

    public async Task<Result<PersonalDataResponse>> Handle(GetPersonalDataQuery request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Email}."));
        }

        var personalData = await context.PersonalData.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

        if (personalData is null)
        {
            return new(new NotFoundException(
                $"Cannot find or user with email {user.Email} has no personal data."));
        }

        return new(PersonalDataMapper.ToPersonalDataResponse(personalData));
    }
}