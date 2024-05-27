using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsByVariantIdQuery(Guid VariantId) : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsByVariantIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsByVariantIdQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsByVariantIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductsByVariantIdAsync(request.VariantId);
            return result;
        }
    }
}
