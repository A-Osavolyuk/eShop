using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByNameQuery(string Name) : IRequest<Result<ProductDTO>>;

    public class GetProductByNameQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByNameQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductDTO>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByNameAsync(request.Name);
            return result;
        }
    }
}
