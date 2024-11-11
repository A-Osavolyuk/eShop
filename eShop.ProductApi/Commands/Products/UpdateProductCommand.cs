using eShop.Application.Mapping;

namespace eShop.ProductApi.Commands.Products;

internal record UpdateProductCommand(UpdateProductRequest Request) : IRequest<Result<UpdateProductResponse>>;

internal sealed class UpdateProductCommandHandler(
    IMongoDatabase database) : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
{
    private readonly IMongoDatabase database = database;

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
                ProductTypes.Clothing => ProductMapper.ToClothingEntity(request.Request),
                ProductTypes.Shoes => ProductMapper.ToShoesEntity(request.Request),
                _ or ProductTypes.None => ProductMapper.ToProductEntity(request.Request)
            };
            
            await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
            
            return new(new UpdateProductResponse
            {
                Message = "Product was updated successfully.",
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}