namespace eShop.FilesStorageApi.Services;

public class StoreService(IConfiguration configuration) : IStoreService
{
    private readonly IConfiguration configuration = configuration;

    private readonly string avatarContainer = configuration["AzureOptions:ContainerNames:AvatarContainer"]!;
    private readonly string productContainer = configuration["AzureOptions:ContainerNames:ProductContainer"]!;
    private readonly string connectionString = configuration["AzureOptions:ConnectionString"]!;

    public async ValueTask<List<string>> GetProductImagesAsync(Guid productId)
    {
        var uriList = new List<string>();
        var blobContainerClient = GetContainerClient(productContainer);
        
        for (int i = 0; true; i++)
        {
            var blobClient = blobContainerClient.GetBlobClient($"{productId}_{i}");

            if (await blobClient.ExistsAsync())
            {
                uriList.Add(blobClient.Uri.ToString());
            }
            else
            {
                break;
            }
        }
        
        return uriList;
    }

    public async ValueTask<List<string>> UploadProductImagesAsync(IReadOnlyCollection<IFormFile> files, Guid productId)
    {
        var uriList = new List<string>();
        var images = files.ToImmutableList();

        for (var i = 0; i < images.Count(); i++)
        {
            var blobContainerClient = GetContainerClient(productContainer);
            var blobClient = blobContainerClient.GetBlobClient($"{productId}_{i}");
            await using (var stream = images[i].OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            uriList.Add(blobClient.Uri.ToString());
        }

        return uriList;
    }

    public async ValueTask DeleteProductImagesAsync(Guid productId)
    {
        var blobContainerClient = GetContainerClient(productContainer);
        for (int i = 0; true; i++)
        {
            var blobClient = blobContainerClient.GetBlobClient($"{productId}_{i}");

            if (await blobClient.ExistsAsync())
                await blobClient.DeleteAsync();
            else
                break;
        }
    }

    private BlobContainerClient GetContainerClient(string containerName)
    {
        var blobServiceClient = new BlobServiceClient(connectionString);
        return blobServiceClient.GetBlobContainerClient(containerName);
    }
}