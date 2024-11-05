namespace eShop.ProductWebApi.Commands;

public record CreateProductCommand(CreateProductRequest Request) : IRequest<Result<CreateProductResponse>>;

public sealed class CreateProductCommandHandler(
    IMongoDatabase database,
    IMapper mapper) : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IMongoDatabase database = database;
    private readonly IMapper mapper = mapper;
    
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");
            
            var product = request.Request.ProductType switch
            {
                ProductTypes.Clothing => mapper.Map<ClothingEntity>(request.Request),
                ProductTypes.Shoes => mapper.Map<ShoesEntity>(request.Request),
                _ or ProductTypes.None => mapper.Map<ProductEntity>(request.Request)
            };
            
            await collection.InsertOneAsync(product, new InsertOneOptions(), cancellationToken);
            return new(new CreateProductResponse()
            {
                Message = "Product created successfully.",
                IsSucceeded = true
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}