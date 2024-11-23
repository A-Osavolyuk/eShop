namespace eShop.ProductApi.Commands.Products;

internal sealed record UpdateProductCommand(UpdateProductRequest Request) : IRequest<Result<UpdateProductResponse>>;

internal sealed class UpdateProductCommandHandler(AppDbContext context)
    : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        if (!await context.Products.AsNoTracking().AnyAsync(x => x.Id == request.Request.Id, cancellationToken))
        {
            return new Result<UpdateProductResponse>(
                new NotFoundException($"Cannot find product with ID {request.Request.Id}"));
        }

        var entity = request.Request.ProductType switch
        {
            ProductTypes.Clothing => ProductMapper.ToClothingEntity(request.Request),
            ProductTypes.Shoes => ProductMapper.ToShoesEntity(request.Request),
            _ or ProductTypes.None => ProductMapper.ToProductEntity(request.Request)
        };

        context.Products.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return new Result<UpdateProductResponse>(new UpdateProductResponse
        {
            Message = "Product was updated successfully.",
        });
    }
}