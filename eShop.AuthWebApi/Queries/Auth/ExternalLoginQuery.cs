
namespace eShop.AuthWebApi.Queries.Auth
{
    public record ExternalLoginQuery(string Provider, string? ReturnUri) : IRequest<Result<ExternalLoginResponse>>;

    public class ExternalLoginQueryHandler(IAuthService authService) : IRequestHandler<ExternalLoginQuery, Result<ExternalLoginResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<ExternalLoginResponse>> Handle(ExternalLoginQuery request, CancellationToken cancellationToken)
        {
            var result = await authService.RequestExternalLogin(request.Provider, request.ReturnUri);
            return result;
        }
    }
}
