namespace eShop.AuthWebApi.Commands.Auth
{
    public record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<Result<RefreshTokenResponse>>;

    public class RefreshTokenCommandHandler(
        ITokenHandler tokenHandler,
        ILogger<RefreshTokenCommandHandler> logger) : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
    {
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly ILogger<RefreshTokenCommandHandler> logger = logger;

        public Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("refresh token");
            try
            {
                logger.LogInformation("Attempting to refresh token. Request ID {requestId}", request.Request.RequestId);
                var newToken = tokenHandler.RefreshToken(request.Request.Token);

                logger.LogInformation("Successfully refreshed token. Request ID {requestId}", request.Request.RequestId);
                return Task.FromResult(new Result<RefreshTokenResponse>(new RefreshTokenResponse() { Token = newToken }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(logger.LogErrorWithException<RefreshTokenResponse>(ex, actionMessage, request.Request.RequestId));
            }
        }
    }
}
