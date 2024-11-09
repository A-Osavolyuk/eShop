using Azure.Storage.Blobs;
using eShop.Domain.DTOs;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    class StoreService(IConfiguration configuration, IHttpClientService clientService) : IStoreService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly IHttpClientService clientService = clientService;
        
        public async ValueTask<string> GetUserAvatarAsync(string userId)
        {
            var blobContainerClient = GetContainerClient(configuration["AvatarContainer"]!);
            var blobClient = blobContainerClient.GetBlobClient($"avatar_{userId}");
            return blobClient.Uri.ToString();
        }

        public async ValueTask<ResponseDto> UploadProductImagesAsync(IReadOnlyList<IBrowserFile> files, Guid productId) =>
            await clientService.SendFilesAsync(
                new FileRequestDto(new FileData(files, productId.ToString(), "productId"), HttpMethods.POST, 
                    $"{configuration["Services:Gateway"]}/api/v1/Files/upload-product-images"));

        public async ValueTask RemoveUserAvatarAsync(string userId)
        {
            var blobContainerClient = GetContainerClient(configuration["AvatarContainer"]!);
            var result = await blobContainerClient.DeleteBlobIfExistsAsync($"avatar_{userId}");
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
