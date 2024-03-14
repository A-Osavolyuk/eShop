namespace eShop.ProductWebApi.Products.Create
{
    public record CreateProductCommand(CreateUpdateProductRequestDto Product) : IRequest<Result<ProductDto>>;

    public class CreateProductCommandHandler(
        IProductsRepository repository, 
        IMapper mapper, 
        IValidator<CreateUpdateProductRequestDto> validator) : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductsRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<CreateUpdateProductRequestDto> validator = validator;

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Product, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(e => e.ErrorMessage)));

            var product = mapper.Map<ProductEntity>(request.Product);
            var result = await repository.CreateProductAsync(product);

            return result.Match<Result<ProductDto>>(s => new(mapper.Map<ProductDto>(s)), f => new(f));
        }
    }
}
