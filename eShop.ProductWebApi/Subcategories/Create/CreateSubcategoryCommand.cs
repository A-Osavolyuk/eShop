namespace eShop.ProductWebApi.Subcategories.Create
{
    public record CreateSubcategoryCommand(SubcategoryDto Subcategory) : IRequest<Result<SubcategoryEntity>>;

    public class CreateSubcategoryCommandHandler(
        ISubcategoriesRepository repository,
        IMapper mapper,
        IValidator<SubcategoryDto> validator)
        : IRequestHandler<CreateSubcategoryCommand, Result<SubcategoryEntity>>
    {
        private readonly ISubcategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<SubcategoryDto> validator = validator;

        public async Task<Result<SubcategoryEntity>> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Subcategory, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage)));

            var subcategory = mapper.Map<SubcategoryEntity>(request.Subcategory);

            var result = await repository.CreateSubcategoryAsync(subcategory);

            return result.Match<Result<SubcategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
