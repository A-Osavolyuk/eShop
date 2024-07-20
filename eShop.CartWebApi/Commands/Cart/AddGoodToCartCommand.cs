namespace eShop.CartWebApi.Commands.Cart
{
    public record AddGoodToCartCommand(AddGoodToCartRequest AddGoodRequest) : IRequest<Result<Unit>>;
    public class AddGoodToCartCommandHandler(ICartRepository repository, IValidator<AddGoodToCartRequest> validator) : IRequestHandler<AddGoodToCartCommand, Result<Unit>>
    {
        private readonly ICartRepository repository = repository;
        private readonly IValidator<AddGoodToCartRequest> validator = validator;

        public async Task<Result<Unit>> Handle(AddGoodToCartCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.AddGoodRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await repository.AddGoodAsync(request.AddGoodRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
