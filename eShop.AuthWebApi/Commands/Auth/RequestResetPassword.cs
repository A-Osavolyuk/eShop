
namespace eShop.AuthWebApi.Commands.Auth
{
    public record RequestResetPasswordCommand(string Email) : IRequest<Result<ResetPasswordResponse>>;

    public class RequestResetPasswordCommandHandler(IAuthService authService) : IRequestHandler<RequestResetPasswordCommand, Result<ResetPasswordResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<ResetPasswordResponse>> Handle(RequestResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.RequestResetPasswordAsync(request.Email);
            return result;
        }
    }
}
