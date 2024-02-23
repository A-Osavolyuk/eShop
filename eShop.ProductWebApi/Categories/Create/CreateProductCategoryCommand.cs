namespace eShop.ProductWebApi.Categories.Create
{
    public record CreateProductCategoryCommand(ProductCategoryDto CategoryDto) : IRequest<Result<CategoryEntity>>;

    public class CreateProductCategoryCommandHandler(
        IValidator<ProductCategoryDto> validator,
        IMapper mapper,
        ICategoriesRepository repository) : IRequestHandler<CreateProductCategoryCommand, Result<CategoryEntity>>
    {
        private readonly IValidator<ProductCategoryDto> validator = validator;
        private readonly IMapper mapper = mapper;
        private readonly ICategoriesRepository repository = repository;

        public async Task<Result<CategoryEntity>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CategoryDto);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToList()));

            var category = mapper.Map<CategoryEntity>(request.CategoryDto);

            var result = await repository.CreateCategoryAsync(category);

            return result.Match<Result<CategoryEntity>>(s => new(s), f => new(f));

        }
    }
}
