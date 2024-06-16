using Azure.Storage.Blobs;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    class StoreService : IStoreService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public StoreService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            _blobContainerName = configuration.GetValue<string>("BlobContainerName")!;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async ValueTask<IEnumerable<string>> AddProductImagesAsync(IReadOnlyList<IBrowserFile> images, Guid productId)
        {
            var uriList = new List<string>();

            for (var i = 0; i < images.Count(); i++)
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
                var blobClient = blobContainerClient.GetBlobClient($"{productId}_{i}");
                using (var stream = images[i].OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }
                uriList.Add(blobClient.Uri.ToString());
            }

            return uriList;
        }
    }
}
