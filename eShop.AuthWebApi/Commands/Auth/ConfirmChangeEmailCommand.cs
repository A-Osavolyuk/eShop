
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmChangeEmailCommand(ConfirmChangeEmailRequest ConfirmChangeEmailRequest) : IRequest<Result<ConfirmChangeEmailResponse>>;

    public class ConfirmChangeEmailCommandHandler(IAuthService authService) : IRequestHandler<ConfirmChangeEmailCommand, Result<ConfirmChangeEmailResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<ConfirmChangeEmailResponse>> Handle(ConfirmChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.ConfirmChangeEmailAsync(request.ConfirmChangeEmailRequest);
            return result;
        }
    }
}
