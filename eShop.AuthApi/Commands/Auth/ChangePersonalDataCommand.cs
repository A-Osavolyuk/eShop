namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ChangePersonalDataCommand(ChangePersonalDataRequest Request)
        : IRequest<Result<ChangePersonalDataResponse>>;

    internal sealed class ChangePersonalDataCommandHandler(
        AppManager appManager,
        AuthDbContext context) : IRequestHandler<ChangePersonalDataCommand, Result<ChangePersonalDataResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly AuthDbContext context = context;

        public async Task<Result<ChangePersonalDataResponse>> Handle(ChangePersonalDataCommand request,
            CancellationToken cancellationToken)
        {
            var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
            }

            var personalData = await context.PersonalData.AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

            if (personalData is null)
            {
                return new(new NotFoundException(
                    $"Cannot find personal data for user with email {request.Request.Email}."));
            }

            personalData = PersonalDataMapper.ToPersonalDataEntity(request.Request) with
            {
                Id = personalData.Id, UserId = user.Id
            };
            context.PersonalData.Update(personalData);
            await context.SaveChangesAsync(cancellationToken);

            return new(PersonalDataMapper.ToChangePersonalDataResponse(personalData));
        }
    }
}