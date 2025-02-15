﻿using eShop.Domain.DTOs;
using eShop.Product.Api.Entities;

namespace eShop.Product.Api.Queries.Products;

internal sealed record GetProductByArticleQuery(string ProductArticle) : IRequest<Result<ProductDto>>;

internal sealed class GetProductByArticleQueryHandler(AppDbContext context, ICacheService cacheService)
    : IRequestHandler<GetProductByArticleQuery, Result<ProductDto>>
{
    private readonly AppDbContext context = context;
    private readonly ICacheService cacheService = cacheService;

    public async Task<Result<ProductDto>> Handle(GetProductByArticleQuery request, CancellationToken cancellationToken)
    {
        var key = $"product-{request.ProductArticle}";
        var cachedEntity = await cacheService.GetAsync<ProductDto>(key);

        if (cachedEntity is null)
        {
            if (string.IsNullOrEmpty(request.ProductArticle) || !decimal.TryParse(request.ProductArticle, out _))
            {
                return new Result<ProductDto>(
                    new BadRequestException($"You must provide a product article in request"));
            }

            var entity = await context.Products
                .AsNoTracking()
                .Include(p => p.Seller)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(x => x.Article == request.ProductArticle, cancellationToken);

            if (entity is null)
            {
                return new Result<ProductDto>(
                    new NotFoundException($"Cannot find product with article {request.ProductArticle}"));
            }

            var response = await Map(entity);

            await cacheService.SetAsync(key, response, TimeSpan.FromMinutes(30));

            return new Result<ProductDto>(response);
        }

        return new(cachedEntity);
    }

    private async Task<ProductDto> Map(ProductEntity entity)
    {
        var response = entity.ProductType switch
        {
            ProductTypes.Shoes => Mapper.ToShoesDto(await FindOfType<ShoesEntity>(entity)),
            ProductTypes.Clothing => Mapper.ToClothingDto(await FindOfType<ClothingEntity>(entity)),
            _ or ProductTypes.None => Mapper.ToProductDto(entity),
        };

        return response;
    }

    private async Task<TEntity> FindOfType<TEntity>(ProductEntity entity) where TEntity : ProductEntity
    {
        var response = await context.Products.AsNoTracking().OfType<TEntity>()
            .FirstOrDefaultAsync(x => x.Article == entity.Article);
        return response!;
    }
}