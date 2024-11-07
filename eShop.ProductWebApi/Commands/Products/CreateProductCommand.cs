using eShop.Application.Mapping;

namespace eShop.ProductWebApi.Commands.Products;

public record CreateProductCommand(CreateProductRequest Request) : IRequest<Result<CreateProductResponse>>;

public sealed class CreateProductCommandHandler(
    IMongoDatabase database) : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IMongoDatabase database = database;
    
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");
            
            var product = request.Request.ProductType switch
            {
                ProductTypes.Clothing => ProductMapper.ToClothingEntity(request.Request),
                ProductTypes.Shoes => ProductMapper.ToShoesEntity(request.Request),
                _ or ProductTypes.None => ProductMapper.ToProductEntity(request.Request)
            };
            
            await collection.InsertOneAsync(product, new InsertOneOptions(), cancellationToken);
            return new(new CreateProductResponse()
            {
                Message = "Product created successfully.",
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}