
namespace eShop.ProductWebApi.Subcategories.Get
{
    public record GetSubcategoriesListQuery : IRequest<Result<IEnumerable<SubcategoryEntity>>>;

    public class GetSubcategoriesListQueryHandler(ISubcategoriesRepository repository) : IRequestHandler<GetSubcategoriesListQuery, Result<IEnumerable<SubcategoryEntity>>>
    {
        private readonly ISubcategoriesRepository repository = repository;

        public async Task<Result<IEnumerable<SubcategoryEntity>>> Handle(GetSubcategoriesListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllSubcategoriesAsync();

            return result.Match<Result<IEnumerable<SubcategoryEntity>>>(s => new(s), f => new(f));
        }
    }
}
