using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Categories.Create
{
    public record CreateProductCategoryCommand(CreateUpdateCategoryRequest Category) : IRequest<Result<CategoryDto>>;

    public class CreateProductCategoryCommandHandler(
        IValidator<CreateUpdateCategoryRequest> validator,
        IMapper mapper,
        ICategoriesRepository repository) : IRequestHandler<CreateProductCategoryCommand, Result<CategoryDto>>
    {
        private readonly IValidator<CreateUpdateCategoryRequest> validator = validator;
        private readonly IMapper mapper = mapper;
        private readonly ICategoriesRepository repository = repository;

        public async Task<Result<CategoryDto>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Category);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage)));

            var category = mapper.Map<CategoryEntity>(request.Category);

            var result = await repository.CreateCategoryAsync(category);

            return result.Match<Result<CategoryDto>>(s => new(mapper.Map<CategoryDto>(s)), f => new(f));

        }
    }
}
