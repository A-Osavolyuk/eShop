using System.Net.Mime;
using eShop.Domain.Exceptions;
using eShop.FilesStorageApi.Services;

namespace eShop.FilesStorageApi.Queries;

internal sealed record GetProductImagesQuery(Guid ProductId) : IRequest<Result<List<string>>>;

internal sealed class GetProductImagesQueryHandler(IStoreService service)
    : IRequestHandler<GetProductImagesQuery, Result<List<string>>>
{
    private readonly IStoreService service = service;

    public async Task<Result<List<string>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
    {
        var response = await service.GetProductImagesAsync(request.ProductId);

        if (!response.Any())
        {
            return new(new NotFoundException($"Cannot find product images for product with ID {request.ProductId}."));
        }

        return response;
    }
}