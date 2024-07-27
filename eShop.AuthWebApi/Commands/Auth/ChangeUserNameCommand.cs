
namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangeUserNameCommand(ChangeUserNameRequest ChangeUserNameRequest) : IRequest<Result<ChangeUserNameResponse>>;

    public class ChangeUserNameCommandHandler(
        IAuthService authService,
        IValidator<ChangeUserNameRequest> validator) : IRequestHandler<ChangeUserNameCommand, Result<ChangeUserNameResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<ChangeUserNameRequest> validator = validator;
        public async Task<Result<ChangeUserNameResponse>> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ChangeUserNameRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.ChangeUserNameAsync(request.ChangeUserNameRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
