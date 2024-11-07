using eShop.Domain.Exceptions;
using eShop.FilesStorageApi.Services;

namespace eShop.FilesStorageApi.Commands;

public record UploadProductImagesCommand(IFormFileCollection Files, Guid ProductId) : IRequest<Result<UploadProductImagesResponse>>;

public class UploadProductImagesCommandHandler(
    IStoreService service) : IRequestHandler<UploadProductImagesCommand, Result<UploadProductImagesResponse>>
{
    private readonly IStoreService service = service;

    public async Task<Result<UploadProductImagesResponse>> Handle(UploadProductImagesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await service.UploadProductImagesAsync(request.Files, request.ProductId);

            if (!list.Any())
            {
                return new(new FailedOperationException($"Cannot upload images for product with ID {request.ProductId}."));
            }

            return new(new UploadProductImagesResponse()
            {
                Images = list,
                Message = "Files were uploaded successfully.",
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}