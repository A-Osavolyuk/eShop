
namespace eShop.ProductWebApi.Subcategories.Get
{
    public record GetSubcategoryByNameQuery(string Name) : IRequest<Result<SubcategoryEntity>>;

    public class GetSubcategoryByNameQueryHandler(ISubcategoriesRepository repository) : IRequestHandler<GetSubcategoryByNameQuery, Result<SubcategoryEntity>>
    {
        private readonly ISubcategoriesRepository repository = repository;

        public async Task<Result<SubcategoryEntity>> Handle(GetSubcategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSubcategoryByNameAsync(request.Name);

            return result.Match<Result<SubcategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
