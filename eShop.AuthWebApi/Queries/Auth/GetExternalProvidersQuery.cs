
namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetExternalProvidersQuery() : IRequest<Result<IEnumerable<ExternalProviderDto>>>;

    public class GetExternalProvidersQueryHandler(IAuthService authService) : IRequestHandler<GetExternalProvidersQuery, Result<IEnumerable<ExternalProviderDto>>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<IEnumerable<ExternalProviderDto>>> Handle(GetExternalProvidersQuery request, CancellationToken cancellationToken)
        {
            var result = await authService.GetExternalProvidersAsync();
            return result;
        }
    }
}
