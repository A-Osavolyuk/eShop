namespace eShop.AuthWebApi.Queries.Auth
{
    public record HandleExternalLoginResponseQuery(ExternalLoginInfo ExternalLoginInfo, string? RemoteError, string? ReturnUri) : IRequest<Result<string>>;

    public class HandleExternalLoginResponseQueryHandler(IAuthService authService) : IRequestHandler<HandleExternalLoginResponseQuery, Result<string>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<string>> Handle(HandleExternalLoginResponseQuery request, CancellationToken cancellationToken)
        {
            var result = await authService.HandleExternalLoginResponseAsync(request.ExternalLoginInfo, request.ReturnUri ?? "/");
            return result;
        }
    }
}
