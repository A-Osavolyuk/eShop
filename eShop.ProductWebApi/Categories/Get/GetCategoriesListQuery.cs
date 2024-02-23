namespace eShop.ProductWebApi.Categories.Get
{
    public record GetCategoriesListQuery : IRequest<Result<IEnumerable<CategoryEntity>>>;

    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, Result<IEnumerable<CategoryEntity>>>
    {
        private readonly ICategoriesRepository repository;

        public GetCategoriesListQueryHandler(ICategoriesRepository categories)
        {
            this.repository = categories;
        }

        public async Task<Result<IEnumerable<CategoryEntity>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllCategoriesAsync();

            return result.Match<Result<IEnumerable<CategoryEntity>>>(s => new(s), f => new(f));
        }
    }
}
