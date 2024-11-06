using eShop.Application.Mapping;
using eShop.Domain.DTOs.Products;

namespace eShop.ProductWebApi.Queries.Products;

public record GetProductByNameQuery(string ProductName) : IRequest<Result<ProductDto>>;

public class GetProductQueryByNameHandler(
    IMongoDatabase database) : IRequestHandler<GetProductByNameQuery, Result<ProductDto>>
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
                ProductTypes.Shoes => ProductMapper.ToShoesDto((ShoesEntity)entity),
                ProductTypes.Clothing => ProductMapper.ToClothingDto((ClothingEntity)entity),
                _ or ProductTypes.None => ProductMapper.ToProductDto(entity),
            };

            return new(response);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}