using eShop.Domain.DTOs.ProductApi;
using eShop.Domain.Entities.ProductApi;

namespace eShop.ProductApi.Queries.Products;

internal sealed record GetProductByIdQuery(Guid ProductId) : IRequest<Result<ProductDto>>;

internal sealed class GetProductByIdQueryHandler(AppDbContext context)
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.ProductId != Guid.Empty)
        {
            var entity = await context.Products
                .AsNoTracking()
                .Include(p => p.Seller)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (entity is null)
            {
                return new Result<ProductDto>(
                    new NotFoundException($"Cannot find product with ID {request.ProductId}"));
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
            return new Result<ProductDto>(new NotFoundException($"Cannot find product with ID, article or name"));
        }
    }
    
    private async Task<TEntity> FindOfType<TEntity>(ProductEntity entity) where TEntity : ProductEntity
    {
        var response = await context.Products.AsNoTracking().OfType<TEntity>()
            .FirstOrDefaultAsync(x => x.Article == entity.Article);
        return response!;
    }
}