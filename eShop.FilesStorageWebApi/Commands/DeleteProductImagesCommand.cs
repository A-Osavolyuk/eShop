namespace eShop.FilesStorageWebApi.Commands;

public record DeleteProductImagesCommand(Guid ProductId) : IRequest<Result<DeleteProductImagesResponse>>;

public class DeleteProductImagesCommandHandler(IStoreService service) : IRequestHandler<DeleteProductImagesCommand, Result<DeleteProductImagesResponse>>
{
    private readonly IStoreService service = service;

    public async Task<Result<DeleteProductImagesResponse>> Handle(DeleteProductImagesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.DeleteProductImagesAsync(request.ProductId);
            
            return new DeleteProductImagesResponse()
            {
                Message = "Product images were deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}