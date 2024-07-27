namespace eShop.AuthWebApi.Commands.Auth
{
    public record RefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : IRequest<Result<RefreshTokenResponse>>;

    public class RefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
    {
        private readonly IAuthService authService = authService;

        public Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = authService.RefreshToken(request.RefreshTokenRequest);
            return Task.FromResult(result);
        }
    }
}
