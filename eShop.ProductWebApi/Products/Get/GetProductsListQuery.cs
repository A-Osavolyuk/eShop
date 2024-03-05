
namespace eShop.ProductWebApi.Products.Get
{
    public record GetProductsListQuery : IRequest<Result<IEnumerable<ProductEntity>>>;

    public class GetProductsListQueryHandler(IProductsRepository repository) : IRequestHandler<GetProductsListQuery, Result<IEnumerable<ProductEntity>>>
    {
        private readonly IProductsRepository repository = repository;

        public async Task<Result<IEnumerable<ProductEntity>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllProductsAsync();

            return result.Match<Result<IEnumerable<ProductEntity>>>(s => new(s), f => new(f));
        }
    }
}
