namespace eShop.ProductWebApi.Commands.Products;

public record UpdateProductCommand(UpdateProductRequest Request) : IRequest<Result<UpdateProductResponse>>;

public class UpdateProductCommandHandler(
    IMongoDatabase database,
    IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
{
    private readonly IMongoDatabase database = database;
    private readonly IMapper mapper = mapper;

    public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");
            
            var productEntity = await collection.Find(x => x.Id == request.Request.Id).FirstOrDefaultAsync(cancellationToken);

            if (productEntity is null)
            {
                return new(new NotFoundException($"Cannot find product with ID {request.Request.Id}"));
            }
            
            var entity = request.Request.ProductType switch
            {
                ProductTypes.Clothing => mapper.Map<ClothingEntity>(request.Request),
                ProductTypes.Shoes => mapper.Map<ShoesEntity>(request.Request),
                _ or ProductTypes.None => mapper.Map<ProductEntity>(request.Request)
            };
            
            await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
            
            return new(new UpdateProductResponse
            {
                Message = "Product was updated successfully.",
                IsSucceeded = true
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}