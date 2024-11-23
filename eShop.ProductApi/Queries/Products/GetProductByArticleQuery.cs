namespace eShop.ProductApi.Queries.Products;

internal sealed record GetProductByArticleQuery(string ProductArticle) : IRequest<Result<ProductDto>>;

internal sealed class GetProductByArticleQueryHandler(AppDbContext context)
    : IRequestHandler<GetProductByArticleQuery, Result<ProductDto>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<ProductDto>> Handle(GetProductByArticleQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ProductArticle) && decimal.TryParse(request.ProductArticle, out _))
        {
            var entity = await context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Article == request.ProductArticle, cancellationToken);

            if (entity is null)
            {
                return new Result<ProductDto>(
                    new NotFoundException($"Cannot find product with article {request.ProductArticle}"));
            }

            var response = entity.ProductType switch
            {
                ProductTypes.Shoes => ProductMapper.ToShoesDto((ShoesEntity)entity),
                ProductTypes.Clothing => ProductMapper.ToClothingDto((ClothingEntity)entity),
                _ or ProductTypes.None => ProductMapper.ToProductDto(entity),
            };

            return new Result<ProductDto>(response);
        }
        else
        {
            return new Result<ProductDto>(new BadRequestException($"You must provide a product article in request"));
        }
    }
}