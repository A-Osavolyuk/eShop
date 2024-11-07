using Azure.Storage.Blobs;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    class StoreService(IConfiguration configuration) : IStoreService
    {
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<IEnumerable<string>> AddProductImagesAsync(IReadOnlyList<IBrowserFile> images, Guid productId)
        {
            var uriList = new List<string>();

            for (var i = 0; i < images.Count(); i++)
            {
                var blobContainerClient = GetContainerClient(configuration["ProductContainer"]!);
                var blobClient = blobContainerClient.GetBlobClient($"{productId}_{i}");
                await using (var stream = images[i].OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }
                uriList.Add(blobClient.Uri.ToString());
            }

            return uriList;
        }

        public async ValueTask<string> GetUserAvatarAsync(string userId)
        {
            var blobContainerClient = GetContainerClient(configuration["AvatarContainer"]!);
            var blobClient = blobContainerClient.GetBlobClient($"avatar_{userId}");
            return blobClient.Uri.ToString();
        }

        public async ValueTask RemoveUserAvatarAsync(string userId)
        {
            var blobContainerClinet = GetContainerClient(configuration["AvatarContainer"]!);
            var result = await blobContainerClinet.DeleteBlobIfExistsAsync($"avatar_{userId}");
        }

        public async ValueTask<string> UploadUserAvatarAsync(string userId, IBrowserFile file)
        {
            var blobContainerClient = GetContainerClient(configuration["AvatarContainer"]!);
            var blobClient = blobContainerClient.GetBlobClient($"avatar_{userId}");
            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return blobClient.Uri.ToString();
        }

        private BlobContainerClient GetContainerClient(string containerName)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            var blobServiceClient = new BlobServiceClient(connectionString);
            return blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}
