namespace eShop.ProductApi.Queries.Products;

internal sealed record GetProductByNameQuery(string ProductName) : IRequest<Result<ProductDto>>;

internal sealed class GetProductQueryByNameHandler(AppDbContext context) : IRequestHandler<GetProductByNameQuery, Result<ProductDto>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                var entity = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == request.ProductName, cancellationToken: cancellationToken);

                if (entity is null)
                {
                    return new Result<ProductDto>(new NotFoundException($"Cannot find product {request.ProductName}"));
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
                return new Result<ProductDto>(new BadRequestException($"You must provide a product name in request"));
            }
        }
        catch (Exception ex)
        {
            return new Result<ProductDto>(ex);
        }
    }
}