namespace eShop.ProductWebApi.Categories.Get
{
    public record GetCategoryByNameQuery(string name) : IRequest<Result<CategoryEntity>>;

    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, Result<CategoryEntity>>
    {
        private readonly ICategoriesRepository repository;

        public GetCategoryByNameQueryHandler(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<CategoryEntity>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetCategoryByNameAsync(request.name);

            return result.Match<Result<CategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
