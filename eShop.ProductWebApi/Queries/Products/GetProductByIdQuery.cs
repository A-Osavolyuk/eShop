using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDTO>>;

    public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByIdAsync(request.Id);
            return result;
        }
    }
}
