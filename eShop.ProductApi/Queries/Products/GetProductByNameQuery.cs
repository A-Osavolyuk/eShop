using eShop.Domain.DTOs.ProductApi;
using eShop.Domain.Entities.ProductApi;

namespace eShop.ProductApi.Queries.Products;

internal sealed record GetProductByNameQuery(string ProductName) : IRequest<Result<ProductDto>>;

internal sealed class GetProductQueryByNameHandler(AppDbContext context)
    : IRequestHandler<GetProductByNameQuery, Result<ProductDto>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ProductName))
        {
            var entity = await context.Products
                .AsNoTracking()
                .Include(p => p.Seller)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(x => x.Name == request.ProductName, cancellationToken);

            if (entity is null)
            {
                return new Result<ProductDto>(new NotFoundException($"Cannot find product {request.ProductName}"));
            }

            var response = entity.ProductType switch
            {
                ProductTypes.Shoes => ProductMapper.ToShoesDto(await FindOfType<ShoesEntity>(entity)),
                ProductTypes.Clothing => ProductMapper.ToClothingDto(await FindOfType<ClothingEntity>(entity)),
                _ or ProductTypes.None => ProductMapper.ToProductDto(entity),
            };

            return new Result<ProductDto>(response);
        }
        else
        {
            return new Result<ProductDto>(new BadRequestException($"You must provide a product name in request"));
        }
    }
    
    private async Task<TEntity> FindOfType<TEntity>(ProductEntity entity) where TEntity : ProductEntity
    {
        var response = await context.Products.AsNoTracking().OfType<TEntity>()
            .FirstOrDefaultAsync(x => x.Article == entity.Article);
        return response!;
    }
}