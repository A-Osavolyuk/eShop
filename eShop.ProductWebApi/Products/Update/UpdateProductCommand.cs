
namespace eShop.ProductWebApi.Products.Update
{
    public record UpdateProductCommand(Guid Id, ProductDto Product) : IRequest<Result<ProductEntity>>;

    public class UpdateProductCommandHandler(
        IProductsRepository repository,
        IMapper mapper,
        IValidator<ProductDto> validator) 
        : IRequestHandler<UpdateProductCommand, Result<ProductEntity>>
    {
        private readonly IProductsRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<ProductDto> validator = validator;

        public async Task<Result<ProductEntity>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Product, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).", 
                    validationResult.Errors.Select(e => e.ErrorMessage)));

            var product = mapper.Map<ProductEntity>(request.Product);
            var result = await repository.UpdateProductAsync(product, request.Id);

            return result.Match<Result<ProductEntity>>(s => new(s), f => new(f));
        }
    }
}
