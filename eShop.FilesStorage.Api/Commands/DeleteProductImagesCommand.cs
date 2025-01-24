using eShop.Domain.Responses.Api.Files;
using eShop.FilesStorage.Api.Services.Interfaces;
using eShop.FilesStorage.Api.Services;

namespace eShop.FilesStorage.Api.Commands;

internal sealed record DeleteProductImagesCommand(Guid ProductId) : IRequest<Result<DeleteProductImagesResponse>>;

internal sealed class DeleteProductImagesCommandHandler(IStoreService service)
    : IRequestHandler<DeleteProductImagesCommand, Result<DeleteProductImagesResponse>>
{
    private readonly IStoreService service = service;

    public async Task<Result<DeleteProductImagesResponse>> Handle(DeleteProductImagesCommand request,
        CancellationToken cancellationToken)
    {
        await service.DeleteProductImagesAsync(request.ProductId);

        return new DeleteProductImagesResponse()
        {
            Message = "Product images were deleted successfully."
        };
    }
}