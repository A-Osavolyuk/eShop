using eShop.Domain.Enums;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByNameWithTypeQuery(string Name, ProductType Type) : IRequest<Result<ProductDTO>>;

    public class GetProductByNameWithTypeQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByNameWithTypeQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductDTO>> Handle(GetProductByNameWithTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByNameWithType(request.Name, request.Type);
            return result;
        }
    }
}
