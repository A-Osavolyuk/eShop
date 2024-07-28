using eShop.Domain.Responses.Product;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record SearchProductByNameQuery(string Name) : IRequest<Result<SearchProductResponse>>;

    public class SearchProductByNameQueryHandler(IProductRepository repository) : IRequestHandler<SearchProductByNameQuery, Result<SearchProductResponse>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<SearchProductResponse>> Handle(SearchProductByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.SearchAsync(request.Name);
            return result;
        }
    }
}
