
namespace eShop.ProductWebApi.Subcategories.Get
{
    public record GetSubcategoryByIdQuery(Guid Id) : IRequest<Result<SubcategoryEntity>>;

    public class GetSubcategoryByIdQueryHandler(ISubcategoriesRepository repository) : IRequestHandler<GetSubcategoryByIdQuery, Result<SubcategoryEntity>>
    {
        private readonly ISubcategoriesRepository repository = repository;

        public async Task<Result<SubcategoryEntity>> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSubcategoryByIdAsync(request.Id);

            return result.Match<Result<SubcategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
