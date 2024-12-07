﻿using Azure.Storage.Blobs;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services
{
    class StoreService(IConfiguration configuration, IHttpClientService clientService) : IStoreService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly IHttpClientService clientService = clientService;

        public async ValueTask<ResponseDto> GetUserAvatarAsync(string userId)=>
            await clientService.SendAsync(
                new RequestDto($"{configuration["Services:Gateway"]}/api/v1/Files/get-user-avatar/{userId}",
                    HttpMethods.GET));

        public async ValueTask<ResponseDto>
            UploadProductImagesAsync(IReadOnlyList<IBrowserFile> files, Guid productId) =>
            await clientService.SendFilesAsync(
                new FileRequestDto(new FileData(files, productId.ToString(), "productId"), HttpMethods.POST,
                    $"{configuration["Services:Gateway"]}/api/v1/Files/upload-product-images"));

        public async ValueTask<ResponseDto> RemoveUserAvatarAsync(string userId) =>
            await clientService.SendAsync(
                new RequestDto($"{configuration["Services:Gateway"]}/api/v1/Files/remove-user-avatar/{userId}",
                    HttpMethods.DELETE));

        public async ValueTask<ResponseDto> UploadUserAvatarAsync(string userId, IBrowserFile file) =>
            await clientService.SendFilesAsync(
                new FileRequestDto(new FileData(file, userId, "userId"), HttpMethods.POST,
                    $"{configuration["Services:Gateway"]}/api/v1/Files/upload-user-avatar"));

        private BlobContainerClient GetContainerClient(string containerName)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            var blobServiceClient = new BlobServiceClient(connectionString);
            return blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}