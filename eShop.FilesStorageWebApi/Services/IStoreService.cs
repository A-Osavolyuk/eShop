namespace eShop.FilesStorageWebApi.Services;

public interface IStoreService
{
    public ValueTask<List<string>> GetProductImagesAsync(Guid productId);
    public ValueTask<List<string>> UploadProductImagesAsync(IReadOnlyCollection<IFormFile> files, Guid productId);
    public ValueTask DeleteProductImagesAsync(Guid productId);
}