
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmChangePhoneNumberCommand(ConfirmChangePhoneNumberRequest ConfirmChangePhoneNumberRequest) : IRequest<Result<ConfirmChangePhoneNumberResponse>>;

    public class ConfirmChangePhoneNumberCommandHandler(
        IAuthService authService) : IRequestHandler<ConfirmChangePhoneNumberCommand, Result<ConfirmChangePhoneNumberResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<ConfirmChangePhoneNumberResponse>> Handle(ConfirmChangePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.ConfirmChangePhoneNumberAsync(request.ConfirmChangePhoneNumberRequest);
            return result;
        }
    }
}
