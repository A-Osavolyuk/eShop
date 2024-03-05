
namespace eShop.ProductWebApi.Products.Get
{
    public record GetProductByNameQuery(string Name) : IRequest<Result<ProductEntity>>;

    public class GetProductByNameQueryHandler(IProductsRepository repository) : IRequestHandler<GetProductByNameQuery, Result<ProductEntity>>
    {
        private readonly IProductsRepository repository = repository;

        public async Task<Result<ProductEntity>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByNameAsync(request.Name);
            return result.Match<Result<ProductEntity>>(s => new(s), f => new(f));
        }
    }
}
