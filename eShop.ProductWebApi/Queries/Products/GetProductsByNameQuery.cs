using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsByNameQuery(string Name) : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsByNameQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsByNameQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductsByNameAsync(request.Name);
            return result;
        }
    }
}
