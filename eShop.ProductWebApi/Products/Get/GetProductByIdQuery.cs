
namespace eShop.ProductWebApi.Products.Get
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductEntity>>;

    public class GetProductByIdQueryHandler(IProductsRepository repository) : IRequestHandler<GetProductByIdQuery, Result<ProductEntity>>
    {
        private readonly IProductsRepository repository = repository;

        public async Task<Result<ProductEntity>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByIdAsync(request.Id);
            return result.Match<Result<ProductEntity>>(s => new(s), f => new(f));
        }
    }
}
