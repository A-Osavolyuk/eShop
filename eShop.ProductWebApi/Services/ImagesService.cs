using Azure.Storage.Blobs;

namespace eShop.ProductWebApi.Services
{

    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            _blobContainerName = configuration.GetValue<string>("BlobContainerName")!;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async ValueTask<IEnumerable<string>> UploadImageAsync(IFormFileCollection images)
        {
            var uriList = new List<string>();
            foreach (var image in images)
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
                var blobClient = blobContainerClient.GetBlobClient(image.FileName);
                using (var stream = image.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }
                uriList.Add(blobClient.Uri.ToString());
            }

            return uriList;
        }

        public async Task<Stream> DownloadImageAsync(string imageName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient(imageName);
            var response = await blobClient.DownloadAsync();
            return response?.Value?.Content;
        }
    }

    public interface IAzureBlobStorageService
    {
        public Task<Stream> DownloadImageAsync(string imageName);
        public ValueTask<IEnumerable<string>> UploadImageAsync(IFormFileCollection images);
    } 
}