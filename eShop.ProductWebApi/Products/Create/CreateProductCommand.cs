
namespace eShop.ProductWebApi.Products.Create
{
    public record CreateProductCommand(ProductDto Product) : IRequest<Result<ProductEntity>>;

    public class CreateProductCommandHandler(
        IProductsRepository repository, 
        IMapper mapper, 
        IValidator<ProductDto> validator) : IRequestHandler<CreateProductCommand, Result<ProductEntity>>
    {
        private readonly IProductsRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<ProductDto> validator = validator;

        public async Task<Result<ProductEntity>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Product, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(e => e.ErrorMessage)));

            var product = mapper.Map<ProductEntity>(request.Product);
            var result = await repository.CreateProductAsync(product);

            return result.Match<Result<ProductEntity>>(s => new(s), f => new(f));
        }
    }
}
