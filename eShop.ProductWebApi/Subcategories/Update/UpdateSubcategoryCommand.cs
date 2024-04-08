using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Subcategories.Update
{
    public record UpdateSubcategoryCommand(CreateUpdateSubcategoryRequest Subcategory, Guid Id) : IRequest<Result<SubcategoryDto>>;

    public class UpdateSubcategoryCommandHandler(
        ISubcategoriesRepository repository,
        IValidator<CreateUpdateSubcategoryRequest> validator,
        IMapper mapper) : IRequestHandler<UpdateSubcategoryCommand, Result<SubcategoryDto>>
    {
        private readonly ISubcategoriesRepository repository = repository;
        private readonly IValidator<CreateUpdateSubcategoryRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SubcategoryDto>> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Subcategory);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToList()));

            var Subcategory = mapper.Map<SubcategoryEntity>(request.Subcategory);

            var result = await repository.UpdateSubcategoryAsync(Subcategory, request.Id);

            return result.Match<Result<SubcategoryDto>>(s => new(mapper.Map<SubcategoryDto>(s)), f => new(f));
        }
    }
}
