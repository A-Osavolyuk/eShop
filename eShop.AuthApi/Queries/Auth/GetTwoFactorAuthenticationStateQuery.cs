namespace eShop.AuthApi.Queries.Auth
{
    internal sealed record GetTwoFactorAuthenticationStateQuery(string Email)
        : IRequest<Result<TwoFactorAuthenticationStateResponse>>;

    internal sealed class GetTwoFactorAuthenticationStateQueryHandler(
        AppManager appManager)
        : IRequestHandler<GetTwoFactorAuthenticationStateQuery, Result<TwoFactorAuthenticationStateResponse>>
    {
        private readonly AppManager appManager = appManager;

        public async Task<Result<TwoFactorAuthenticationStateResponse>> Handle(
            GetTwoFactorAuthenticationStateQuery request, CancellationToken cancellationToken)
        {
            var user = await appManager.UserManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with email {request.Email}."));
            }

            return new(new TwoFactorAuthenticationStateResponse()
            {
                TwoFactorAuthenticationState = user.TwoFactorEnabled
            });
        }
    }
}