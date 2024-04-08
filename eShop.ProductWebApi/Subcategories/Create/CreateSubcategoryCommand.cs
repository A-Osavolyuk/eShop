using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Subcategories.Create
{
    public record CreateSubcategoryCommand(CreateUpdateSubcategoryRequest Subcategory) : IRequest<Result<SubcategoryDto>>;

    public class CreateSubcategoryCommandHandler(
        ISubcategoriesRepository repository,
        IMapper mapper,
        IValidator<CreateUpdateSubcategoryRequest> validator)
        : IRequestHandler<CreateSubcategoryCommand, Result<SubcategoryDto>>
    {
        private readonly ISubcategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<CreateUpdateSubcategoryRequest> validator = validator;

        public async Task<Result<SubcategoryDto>> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Subcategory, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage)));

            var subcategory = mapper.Map<SubcategoryEntity>(request.Subcategory);

            var result = await repository.CreateSubcategoryAsync(subcategory);

            return result.Match<Result<SubcategoryDto>>(s => new(mapper.Map<SubcategoryDto>(s)), f => new(f));
        }
    }
}
