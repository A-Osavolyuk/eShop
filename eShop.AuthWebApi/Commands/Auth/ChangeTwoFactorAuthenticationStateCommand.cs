
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangeTwoFactorAuthenticationStateCommand(ChangeTwoFactorAuthenticationRequest ChangeTwoFactorAuthenticationRequest) : IRequest<Result<ChangeTwoFactorAuthenticationResponse>>;

    public class ChangeTwoFactorAuthenticationStateCommandHandler(
        IAuthService authService) : IRequestHandler<ChangeTwoFactorAuthenticationStateCommand, Result<ChangeTwoFactorAuthenticationResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<ChangeTwoFactorAuthenticationResponse>> Handle(ChangeTwoFactorAuthenticationStateCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.ChangeTwoFactorAuthenticationStateAsync(request.ChangeTwoFactorAuthenticationRequest);
            return result;
        }
    }
}
