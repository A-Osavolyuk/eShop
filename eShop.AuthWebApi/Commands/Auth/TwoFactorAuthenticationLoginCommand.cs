
namespace eShop.AuthWebApi.Commands.Auth
{
    public record TwoFactorAuthenticationLoginCommand(TwoFactorAuthenticationLoginRequest TwoFactorAuthenticationLoginRequest) : IRequest<Result<LoginResponse>>;

    public class TwoFactorAuthenticationLoginCommandHandler(IAuthService authService) : IRequestHandler<TwoFactorAuthenticationLoginCommand, Result<LoginResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<LoginResponse>> Handle(TwoFactorAuthenticationLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.LoginWithTwoFactorAuthenticationCodeAsync(request.TwoFactorAuthenticationLoginRequest);
            return result;
        }
    }
}
