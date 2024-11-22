namespace eShop.ProductApi.Queries.Products;

internal sealed record GetProductsQuery() : IRequest<Result<List<ProductDto>>>;

internal sealed class GetProductsQueryHandler(AppDbContext context) : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await context.Products.AsNoTracking().ToListAsync(cancellationToken);
            if (products.Any())
            {
                var response = products
                    .Select(ProductMapper.ToProductDto)
                    .ToList();
                
                return new Result<List<ProductDto>>(response);
            }
            return new Result<List<ProductDto>>(new List<ProductDto>());
        }
        catch (Exception ex)
        {
            return new Result<List<ProductDto>>(ex);
        }
    }
}