using eShop.Domain.DTOs.Responses;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record SearchProductByArticleQuery(long Article) : IRequest<Result<ProductExistsResponse>>;

    public class SearchProductByArticleQueryHandler(IProductRepository repository) : IRequestHandler<SearchProductByArticleQuery, Result<ProductExistsResponse>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductExistsResponse>> Handle(SearchProductByArticleQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.SearchAsync(request.Article);
            return result;
        }
    }
}
