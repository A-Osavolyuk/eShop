
namespace eShop.CartWebApi.Commands.Cart
{
    public record UpdateCartCommand(UpdateCartRequest UpdateCartRequest) : IRequest<Result<Unit>>;

    public class UpdateCartCommandHandler(ICartRepository repository, IValidator<UpdateCartRequest> validator) : IRequestHandler<UpdateCartCommand, Result<Unit>>
    {
        private readonly ICartRepository repository = repository;
        private readonly IValidator<UpdateCartRequest> validator = validator;

        public async Task<Result<Unit>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.UpdateCartRequest, cancellationToken);

            if (validationResult.IsValid) 
            {
                var result = await repository.UpdateCartAsync(request.UpdateCartRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
