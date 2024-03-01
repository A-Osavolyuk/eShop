namespace eShop.ProductWebApi.Subcategories.Update
{
    public record UpdateSubcategoryCommand(SubcategoryDto Subcategory, Guid Id) : IRequest<Result<SubcategoryEntity>>;

    public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, Result<SubcategoryEntity>>
    {
        private readonly ISubcategoriesRepository repository;
        private readonly IValidator<SubcategoryDto> validator;
        private readonly IMapper mapper;

        public UpdateSubcategoryCommandHandler(
            ISubcategoriesRepository repository,
            IValidator<SubcategoryDto> validator,
            IMapper mapper)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Result<SubcategoryEntity>> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Subcategory);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToList()));

            var Subcategory = mapper.Map<SubcategoryEntity>(request.Subcategory);

            var result = await repository.UpdateSubcategoryAsync(Subcategory, request.Id);

            return result.Match<Result<SubcategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
