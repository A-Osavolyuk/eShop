using eShop.Domain.Enums;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsListByTypeQuery(ProductType type) : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsListByTypeQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsListByTypeQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsListByTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductsListByTypeAsync(request.type);
            return result;
        }
    }
}
