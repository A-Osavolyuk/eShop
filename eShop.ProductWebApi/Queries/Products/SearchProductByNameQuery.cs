using eShop.Domain.DTOs.Responses;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record SearchProductByNameQuery(string Name) : IRequest<Result<ProductExistsResponse>>;

    public class SearchProductByNameQueryHandler(IProductRepository repository) : IRequestHandler<SearchProductByNameQuery, Result<ProductExistsResponse>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<ProductExistsResponse>> Handle(SearchProductByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.SearchAsync(request.Name);
            return result;
        }
    }
}
