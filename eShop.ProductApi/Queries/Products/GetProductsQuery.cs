using AutoMapper.QueryableExtensions;
using eShop.Application.Mapping;
using eShop.Domain.DTOs.Products;

namespace eShop.ProductApi.Queries.Products;

public record GetProductsQuery() : IRequest<Result<IEnumerable<ProductDto>>>;

public class GetProductsQueryHandler(
    IMongoDatabase database) : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");
            var products = await collection.Find(x => true).ToListAsync(cancellationToken);
            if (products.Any())
            {
                var response = products
                    .Select(ProductMapper.ToProductDto)
                    .ToList();
                
                return new(response);
            }
            return new(Enumerable.Empty<ProductDto>());
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}