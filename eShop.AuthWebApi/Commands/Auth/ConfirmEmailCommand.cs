
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmEmailCommand(ConfirmEmailRequest ConfirmEmailRequest) : IRequest<Result<ConfirmEmailResponse>>;

    public class ConfirmEmailCommandHandler(IAuthService authService) : IRequestHandler<ConfirmEmailCommand, Result<ConfirmEmailResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.ConfirmEmailAsync(request.ConfirmEmailRequest);
            return result;
        }
    }
}
