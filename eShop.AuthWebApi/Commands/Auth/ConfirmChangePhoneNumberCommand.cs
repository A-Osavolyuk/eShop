
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmChangePhoneNumberCommand(ConfirmChangePhoneNumberRequest ConfirmChangePhoneNumberRequest) : IRequest<Result<ConfirmChangePhoneNumberResponse>>;

    public class ConfirmChangePhoneNumberCommandHandler(
        IAuthService authService, 
        IValidator<ConfirmChangePhoneNumberRequest> validator) : IRequestHandler<ConfirmChangePhoneNumberCommand, Result<ConfirmChangePhoneNumberResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ConfirmChangePhoneNumberRequest> validator = validator;

        public async Task<Result<ConfirmChangePhoneNumberResponse>> Handle(ConfirmChangePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ConfirmChangePhoneNumberRequest, cancellationToken);

            if (validationResult.IsValid) 
            {
                var result = await authService.ConfirmChangePhoneNumberAsync(request.ConfirmChangePhoneNumberRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
