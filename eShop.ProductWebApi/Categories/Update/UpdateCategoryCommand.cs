namespace eShop.ProductWebApi.Categories.Update
{
    public record UpdateCategoryCommand(CreateUpdateCategoryRequestDto Category, Guid Id) : IRequest<Result<CategoryDto>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoriesRepository repository;
        private readonly IValidator<CreateUpdateCategoryRequestDto> validator;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(
            ICategoriesRepository repository,
            IValidator<CreateUpdateCategoryRequestDto> validator,
            IMapper mapper)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Category);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToList()));

            var category = mapper.Map<CategoryEntity>(request.Category);

            var result = await repository.UpdateCategoryAsync(category, request.Id);

            return result.Match<Result<CategoryDto>>(s => new(mapper.Map<CategoryDto>(s)), f => new(f));
        }
    }
}
