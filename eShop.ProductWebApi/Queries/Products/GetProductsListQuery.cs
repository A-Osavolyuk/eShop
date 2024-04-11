using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsListQuery : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsListQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsListQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductsListAsync();

            return result;
        }
    }
}
