using eShop.Domain.DTOs.ProductApi;

namespace eShop.ProductApi.Queries.Products;

internal sealed record GetProductsQuery() : IRequest<Result<List<ProductDto>>>;

internal sealed class GetProductsQueryHandler(AppDbContext context)
    : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(p => p.Seller)
            .Include(p => p.Brand)
            .ToListAsync(cancellationToken);
        if (products.Any())
        {
            var response = products
                .Select(ProductMapper.ToProductDto)
                .ToList();

            return new Result<List<ProductDto>>(response);
        }

        return new Result<List<ProductDto>>(new List<ProductDto>());
    }
}