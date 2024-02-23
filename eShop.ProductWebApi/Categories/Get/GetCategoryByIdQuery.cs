namespace eShop.ProductWebApi.Categories.Get
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryEntity>>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryEntity>>
    {
        private readonly ICategoriesRepository repository;

        public GetCategoryByIdQueryHandler(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<CategoryEntity>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetCategoryByIdAsync(request.id);

            return result.Match<Result<CategoryEntity>>(s => new(s), f => new(f));
        }
    }
}
