using eShop.Domain.DTOs;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Domain.Requests.Product;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class ProductService(
        IHttpClientService clientService,
        IConfiguration configuration) : IProductService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> CreateProductAsync(CreateProductRequest request) =>
            await clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/create-product",
                    Data: request, Method: HttpMethods.POST));

        public async ValueTask<ResponseDTO> UpdateProductAsync(UpdateProductRequest request) =>
            await clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/update-product",
                    Data: request, Method: HttpMethods.PUT));

        public async ValueTask<ResponseDTO> DeleteProductAsync(DeleteProductRequest request) =>
            await clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/delete-product",
                    Data: request, Method: HttpMethods.DELETE));

        public async ValueTask<ResponseDTO> GetProductsAsync() => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/get-products",
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByNameAsync(string name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/get-product-by-name/{name}",
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByArticleAsync(string article) => await clientService.SendAsync(
            new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/get-product-by-article/{article}",
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByIdAsync(Guid id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Prodcuts/get-product-by-id/{id}",
                Method: HttpMethods.GET));
    }
}