using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class ProductService(
        IHttpClientService clientService,
        IConfiguration configuration) : IProductService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> CreateProductAsync(CreateClothingRequest request) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/create-product-clothing", Method: HttpMethods.POST, Data: request));

        public async ValueTask<ResponseDTO> CreateProductAsync(CreateShoesRequest request) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/create-product-shoes", Method: HttpMethods.POST, Data: request));

        public async ValueTask<ResponseDTO> DeleteProductAsync(Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/delete-by-id/{Id}", Method: HttpMethods.DELETE));

        public async ValueTask<ResponseDTO> GetProductByIdAsync(Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-id/{Id}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByArticleAsync(long Article) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-article/{Article}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByIdWithTypeAsync(Guid Id, ProductType Type) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-id/{Id}/with-type/{Type}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByNameAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-name/{Name}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByNameWithTypeAsync(string Name, ProductType Type) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-name/{Name}/with-type/{Type}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductsListAsync() => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> UpdateProductAsync(UpdateClothingRequest request) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/update-product-clothing", Method: HttpMethods.PUT, Data: request));

        public async ValueTask<ResponseDTO> UpdateProductAsync(UpdateShoesRequest request) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/update-product-clothing", Method: HttpMethods.PUT, Data: request));
    }
}
