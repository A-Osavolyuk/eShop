namespace eShop.ProductApi.Commands.Products;

internal sealed record CreateProductCommand(CreateProductRequest Request) : IRequest<Result<CreateProductResponse>>;

internal sealed class CreateProductCommandHandler(
    AppDbContext context) : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var entity = request.Request.ProductType switch
        {
            ProductTypes.Clothing => ProductMapper.ToClothingEntity(request.Request),
            ProductTypes.Shoes => ProductMapper.ToShoesEntity(request.Request),
            _ or ProductTypes.None => ProductMapper.ToProductEntity(request.Request)
        };

        await context.Products.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new(new CreateProductResponse()
        {
            Message = "Product created successfully"
        });
    }
}