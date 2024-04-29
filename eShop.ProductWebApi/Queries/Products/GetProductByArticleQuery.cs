using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByArticleQuery(long Article ) : IRequest<Result<ProductDTO>>;

    public class GetProductByArticleQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByArticleQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductDTO>> Handle(GetProductByArticleQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByArticleAsync(request.Article );
            return result;
        }
    }
}
