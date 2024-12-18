using eShop.Domain.DTOs.AuthApi;

namespace eShop.AuthApi.Queries.Auth;

internal sealed record GetExternalProvidersQuery() : IRequest<Result<IEnumerable<ExternalProviderDto>>>;

internal sealed class GetExternalProvidersQueryHandler(
    AppManager appManager) : IRequestHandler<GetExternalProvidersQuery, Result<IEnumerable<ExternalProviderDto>>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<IEnumerable<ExternalProviderDto>>> Handle(GetExternalProvidersQuery request,
        CancellationToken cancellationToken)
    {
        var schemes = await appManager.SignInManager.GetExternalAuthenticationSchemesAsync();
        var providers = schemes.Select(p => new ExternalProviderDto() { Name = p.Name });

        return new(providers);
    }
}