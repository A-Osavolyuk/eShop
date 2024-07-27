
namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetTwoFactorAuthenticationStateQuery(string Email) : IRequest<Result<TwoFactorAuthenticationStateResponse>>;
    public class GetTwoFactorAuthenticationStateQueryHandler(IAuthService authService) : IRequestHandler<GetTwoFactorAuthenticationStateQuery, Result<TwoFactorAuthenticationStateResponse>>
    {
        private readonly IAuthService authService = authService;
        public async Task<Result<TwoFactorAuthenticationStateResponse>> Handle(GetTwoFactorAuthenticationStateQuery request, CancellationToken cancellationToken)
        {
            var result = await authService.GetTwoFactorAuthenticationStateAsync(request.Email);
            return result;
        }
    }
}
