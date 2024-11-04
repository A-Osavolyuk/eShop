using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MongoDB.Driver;

namespace eShop.ProductWebApi.Queries.Products;

public record GetProductsQuery() : IRequest<Result<IEnumerable<ProductDto>>>;

public class GetProductsQueryHandler(
    IMongoDatabase database,
    IMapper mapper) : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IMongoDatabase database = database;
    private readonly IMapper mapper = mapper;

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");
            var products = await collection.Find(x => true).ToListAsync(cancellationToken);
            if (products.Any())
            {
                var response = await products
                    .AsQueryable()
                    .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                
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