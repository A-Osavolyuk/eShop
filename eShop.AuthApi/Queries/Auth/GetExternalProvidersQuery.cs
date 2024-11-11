namespace eShop.AuthApi.Queries.Auth
{
    internal sealed record GetExternalProvidersQuery() : IRequest<Result<IEnumerable<ExternalProviderDto>>>;

    internal sealed class GetExternalProvidersQueryHandler(
        ILogger<GetExternalProvidersQueryHandler> logger,
        AppManager appManager) : IRequestHandler<GetExternalProvidersQuery, Result<IEnumerable<ExternalProviderDto>>>
    {
        private readonly ILogger<GetExternalProvidersQueryHandler> logger = logger;
        private readonly AppManager appManager = appManager;

        public async Task<Result<IEnumerable<ExternalProviderDto>>> Handle(GetExternalProvidersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Attempting to get external login providers");

                var schemes = await appManager.SignInManager.GetExternalAuthenticationSchemesAsync();
                var providers = schemes.Select(p => new ExternalProviderDto() { Name = p.Name });

                logger.LogInformation("Successfully found external providers");
                return new(providers);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<ExternalProviderDto>>(ex, new("find external login providers"));
            }
        }
    }
}
