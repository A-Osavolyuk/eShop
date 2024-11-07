namespace eShop.ProductApi.Commands.Products;

public record DeleteProductCommand(DeleteProductRequest Request) : IRequest<Result<DeleteProductResponse>>;

public class DeleteProductCommandHandler(
    IMongoDatabase database) : IRequestHandler<DeleteProductCommand, Result<DeleteProductResponse>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<ProductEntity>("Products");
            var product = await collection
                .FindOneAndDeleteAsync(x => x.Id == request.Request.ProductId, cancellationToken: cancellationToken);

            if (product is null)
            {
                return new(new NotFoundException($"Cannot find product with ID {request.Request.ProductId}"));
            }

            return new(new DeleteProductResponse()
            {
                Message = "Product was successfully deleted",
            });

        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}