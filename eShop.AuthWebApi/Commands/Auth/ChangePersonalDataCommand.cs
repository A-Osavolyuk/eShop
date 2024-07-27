
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangePersonalDataCommand(ChangePersonalDataRequest ChangePersonalDataRequest) : IRequest<Result<ChangePersonalDataResponse>>;

    public class ChangePersonalDataCommandHandler(
        IAuthService authService,
        IValidator<ChangePersonalDataRequest> validator) : IRequestHandler<ChangePersonalDataCommand, Result<ChangePersonalDataResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ChangePersonalDataRequest> validator = validator;

        public async Task<Result<ChangePersonalDataResponse>> Handle(ChangePersonalDataCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ChangePersonalDataRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.ChangePersonalDataAsync(request.ChangePersonalDataRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
