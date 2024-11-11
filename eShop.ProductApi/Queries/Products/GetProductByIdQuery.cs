using eShop.Application.Mapping;
using eShop.Domain.DTOs.Products;

namespace eShop.ProductApi.Queries.Products;

internal record GetProductByIdQuery(Guid ProductId) : IRequest<Result<ProductDto>>;

internal sealed class GetProductByIdQueryHandler(
    IMongoDatabase database) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");

            ProductEntity entity;
            
            if (request.ProductId != Guid.Empty)
            {
                entity = await collection.Find(x => x.Id == request.ProductId).FirstOrDefaultAsync(cancellationToken);

                if (entity is null)
                {
                    return new(new NotFoundException($"Cannot find product with ID {request.ProductId}"));
                }
            }
            else
            {
                return new (new NotFoundException($"Cannot find product with ID, article or name"));
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