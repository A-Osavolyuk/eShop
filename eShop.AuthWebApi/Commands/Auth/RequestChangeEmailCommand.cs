
namespace eShop.AuthWebApi.Commands.Auth
{
    public record RequestChangeEmailCommand(ChangeEmailRequest ChangeEmailRequest) : IRequest<Result<ChangeEmailResponse>>;

    public class RequestChangeEmailCommandHandler(IAuthService authService, IValidator<ChangeEmailRequest> validator) : IRequestHandler<RequestChangeEmailCommand, Result<ChangeEmailResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ChangeEmailRequest> validator = validator;

        public async Task<Result<ChangeEmailResponse>> Handle(RequestChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ChangeEmailRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.RequestChangeEmailAsync(request.ChangeEmailRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
