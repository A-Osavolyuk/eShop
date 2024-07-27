namespace eShop.AuthWebApi.Commands.Auth
{
    public record RegisterCommand(RegistrationRequest RegistrationRequest) : IRequest<Result<RegistrationResponse>>;

    public class RegisterCommandHandler(IAuthService authService, IValidator<RegistrationRequest> validator) : IRequestHandler<RegisterCommand, Result<RegistrationResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<RegistrationRequest> validator = validator;

        public async Task<Result<RegistrationResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.RegistrationRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.RegisterAsync(request.RegistrationRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
