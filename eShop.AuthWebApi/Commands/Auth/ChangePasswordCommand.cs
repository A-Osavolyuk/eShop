
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangePasswordCommand(ChangePasswordRequest ChangePasswordRequest) : IRequest<Result<ChangePasswordResponse>>;

    public class ChangePasswordCommandHandler(
        IAuthService authService,
        IValidator<ChangePasswordRequest> validator) : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ChangePasswordRequest> validator = validator;

        async Task<Result<ChangePasswordResponse>> IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>.Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ChangePasswordRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.ChangePasswordAsync(request.ChangePasswordRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
