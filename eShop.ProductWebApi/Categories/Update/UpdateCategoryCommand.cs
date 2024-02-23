namespace eShop.ProductWebApi.Categories.Update
{
    public record UpdateCategoryCommand(CategoryDto Category, Guid Id) : IRequest<Result<CategoryEntity>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryEntity>>
    {
        private readonly ICategoriesRepository repository;
        private readonly IValidator<CategoryDto> validator;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(
            ICategoriesRepository repository,
            IValidator<CategoryDto> validator,
            IMapper mapper)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Result<CategoryEntity>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Category);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToList()));

            var category = mapper.Map<CategoryEntity>(request.Category);

            var result = await repository.UpdateCategoryAsync(category, request.Id);

            return result.Match<Result<CategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
