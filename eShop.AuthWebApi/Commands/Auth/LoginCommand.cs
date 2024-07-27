using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.DTOs.Responses.Auth;
using MediatR;

namespace eShop.AuthWebApi.Commands.Auth
{
    public record LoginCommand(LoginRequest loginRequest) : IRequest<Result<LoginResponse>>;

    public class LoginCommandHandler(IAuthService authService, IValidator<LoginRequest> validator) : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IAuthService authService = authService;
        private readonly IValidator<LoginRequest> validator = validator;

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.loginRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await authService.LoginAsync(request.loginRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
