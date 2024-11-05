using eShop.Domain.DTOs.Products;

namespace eShop.ProductWebApi.Queries.Products;

public record GetProductByNameQuery(string ProductName) : IRequest<Result<ProductDto>>;

public class GetProductQueryByNameHandler(
    IMongoDatabase database,
    IMapper mapper) : IRequestHandler<GetProductByNameQuery, Result<ProductDto>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");

            ProductEntity entity;

            if (!string.IsNullOrEmpty(request.ProductName))
            {
                entity = await collection.Find(x => x.Name == request.ProductName).FirstOrDefaultAsync(cancellationToken);

                if (entity is null)
                {
                    return new(new NotFoundException($"Cannot find product {request.ProductName}"));
                }
            }
            else
            {
                return new(new BadRequestException($"You must provide a product name in request"));
            }

            var response = entity.ProductType switch
            {
                ProductTypes.Shoes => mapper.Map<ShoesDto>(entity),
                ProductTypes.Clothing => mapper.Map<ClothingDto>(entity),
                _ or ProductTypes.None => mapper.Map<ProductDto>(entity),
            };

            return new(response);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}