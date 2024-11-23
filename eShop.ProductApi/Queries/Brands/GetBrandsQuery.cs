namespace eShop.ProductApi.Queries.Brands;

internal sealed record GetBrandsQuery() : IRequest<Result<List<BrandDto>>>;

internal sealed class GetBrandsQueryHandler(AppDbContext context)
    : IRequestHandler<GetBrandsQuery, Result<List<BrandDto>>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<List<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await context.Brands.AsNoTracking().ToListAsync(cancellationToken);
        var response = brands.Select(BrandMapper.ToBrandDto).ToList();
        return response;
    }
}