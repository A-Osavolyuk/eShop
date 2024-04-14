using eShop.Domain.Enums;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByIdWithTypeQuery(Guid Id, ProductType Type) : IRequest<Result<ProductDTO>>;

    public class GetProductByIdWithTypeQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdWithTypeQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductDTO>> Handle(GetProductByIdWithTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByIdWithType(request.Id, request.Type);
            return result;
        }
    }
}
