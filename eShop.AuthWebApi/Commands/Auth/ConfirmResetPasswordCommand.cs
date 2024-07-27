
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmResetPasswordCommand(ConfirmResetPasswordRequest ConfirmResetPasswordRequest) : IRequest<Result<ConfirmResetPasswordResponse>>;

    public class ConfirmResetPasswordCommandHandler(IAuthService authService, IValidator<ConfirmResetPasswordRequest> validator) : IRequestHandler<ConfirmResetPasswordCommand, Result<ConfirmResetPasswordResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ConfirmResetPasswordRequest> validator = validator;

        public async Task<Result<ConfirmResetPasswordResponse>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ConfirmResetPasswordRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.ConfirmResetPasswordAsync(request.ConfirmResetPasswordRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
