using eShop.Application.Mapping;
using eShop.Domain.DTOs.Products;

namespace eShop.ProductWebApi.Queries.Products;

public record GetProductByArticleQuery(string ProductArticle) : IRequest<Result<ProductDto>>;

public class GetProductByArticleQueryHandler(
    IMongoDatabase database) : IRequestHandler<GetProductByArticleQuery, Result<ProductDto>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<ProductDto>> Handle(GetProductByArticleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");

            ProductEntity entity;
            
            if (!string.IsNullOrEmpty(request.ProductArticle) && decimal.TryParse(request.ProductArticle, out _))
            {
                entity = await collection.Find(x => x.Article == request.ProductArticle).FirstOrDefaultAsync(cancellationToken);

                if (entity is null)
                {
                    return new(new NotFoundException($"Cannot find product with article {request.ProductArticle}"));
                }
            }
            else
            {
                return new(new BadRequestException($"You must provide a product article in request"));
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