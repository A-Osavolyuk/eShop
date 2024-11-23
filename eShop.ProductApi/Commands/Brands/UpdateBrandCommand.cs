namespace eShop.ProductApi.Commands.Brands;

internal sealed record UpdateBrandCommand(UpdateBrandRequest Request) : IRequest<Result<UpdateBrandResponse>>;

internal sealed class UpdateBrandCommandHandler(
    AppDbContext context) : IRequestHandler<UpdateBrandCommand, Result<UpdateBrandResponse>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<UpdateBrandResponse>> Handle(UpdateBrandCommand request,
        CancellationToken cancellationToken)
    {
        if (!await context.Brands.AsNoTracking().AnyAsync(x => x.Id == request.Request.Id, cancellationToken))
        {
            return new Result<UpdateBrandResponse>(
                new NotFoundException($"Cannot find brand with ID {request.Request.Id}"));
        }

        var entity = BrandMapper.ToBrandEntity(request.Request);
        context.Brands.Update(entity);
        await context.SaveChangesAsync(cancellationToken);

        return new Result<UpdateBrandResponse>(new UpdateBrandResponse()
        {
            Message = "Brand was successfully updated"
        });
    }
}