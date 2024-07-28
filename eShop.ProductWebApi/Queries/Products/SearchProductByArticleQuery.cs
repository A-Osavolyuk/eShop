using eShop.Domain.Responses.Product;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record SearchProductByArticleQuery(long Article) : IRequest<Result<SearchProductResponse>>;

    public class SearchProductByArticleQueryHandler(IProductRepository repository) : IRequestHandler<SearchProductByArticleQuery, Result<SearchProductResponse>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<SearchProductResponse>> Handle(SearchProductByArticleQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.SearchAsync(request.Article);
            return result;
        }
    }
}
