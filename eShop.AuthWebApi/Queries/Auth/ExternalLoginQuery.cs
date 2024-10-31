
using eShop.AuthWebApi.Utilities;

namespace eShop.AuthWebApi.Queries.Auth
{
    public record ExternalLoginQuery(string Provider, string? ReturnUri) : IRequest<Result<ExternalLoginResponse>>;

    public class ExternalLoginQueryHandler(
        ILogger<ExternalLoginQueryHandler> logger,
        AppManager appManager) : IRequestHandler<ExternalLoginQuery, Result<ExternalLoginResponse>>
    {
        private readonly ILogger<ExternalLoginQueryHandler> logger = logger;
        private readonly AppManager appManager = appManager;

        public async Task<Result<ExternalLoginResponse>> Handle(ExternalLoginQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("log in with external provider {0}", request.Provider);
            try
            {
                logger.LogInformation("Attempting to log in with external provider {provider}.", request.Provider);
                var providers = await appManager.SignInManager.GetExternalAuthenticationSchemesAsync();

                var validProvider = providers.Any(x => x.DisplayName == request.Provider);

                if (!validProvider)
                {
                    return logger.LogInformationWithException<ExternalLoginResponse>(
                        new BadRequestException($"Invalid external provider {request.Provider}."), actionMessage);
                }

                var handlerUri = UrlGenerator.Action("handle-external-login-response", "Auth", new { ReturnUri = request.ReturnUri ?? "/" });
                var properties = appManager.SignInManager.ConfigureExternalAuthenticationProperties(request.Provider, handlerUri);

                logger.LogInformation("Successfully logged in with external provider {provider}", request.Provider);
                return new(new ExternalLoginResponse()
                {
                    Provider = request.Provider,
                    AuthenticationProperties = properties
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ExternalLoginResponse>(ex, actionMessage);
            }
        }
    }
}
