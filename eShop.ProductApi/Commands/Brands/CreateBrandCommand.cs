namespace eShop.ProductApi.Commands.Brands;

internal sealed record CreateBrandCommand(CreateBrandRequest Request) : IRequest<Result<CreateBrandResponse>>;

internal sealed class CreateBrandCommandHandle(
    AppDbContext context) : IRequestHandler<CreateBrandCommand, Result<CreateBrandResponse>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<CreateBrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        //var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = BrandMapper.ToBrandEntity(request.Request);
            await context.Brands.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            //await transaction.CommitAsync(cancellationToken);

            return new Result<CreateBrandResponse>(new CreateBrandResponse()
            {
                Message = "Successfully created brand"
            });
        }
        catch (Exception ex)
        {
            //await transaction.RollbackAsync(cancellationToken);
            return new Result<CreateBrandResponse>(ex);
        }
    }
}