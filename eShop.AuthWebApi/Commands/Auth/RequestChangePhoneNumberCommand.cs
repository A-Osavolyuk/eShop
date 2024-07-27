
namespace eShop.AuthWebApi.Commands.Auth
{
    public record RequestChangePhoneNumberCommand(ChangePhoneNumberRequest ChangePhoneNumberRequest) : IRequest<Result<ChangePhoneNumberResponse>>;

    public class RequestChangePhoneNumberCommandHandler(
        IAuthService authService,
        IValidator<ChangePhoneNumberRequest> validator) : IRequestHandler<RequestChangePhoneNumberCommand, Result<ChangePhoneNumberResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ChangePhoneNumberRequest> validator = validator;

        public async Task<Result<ChangePhoneNumberResponse>> Handle(RequestChangePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ChangePhoneNumberRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.RequestChangePhoneNumberAsync(request.ChangePhoneNumberRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
