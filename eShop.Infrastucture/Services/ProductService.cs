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

        public ValueTask<ResponseDTO> CreateProductAsync(CreateClothingRequest request) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/create-product-clothing", Method: HttpMethods.POST, Data: request));

        public ValueTask<ResponseDTO> CreateProductAsync(CreateShoesRequest request) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/create-product-shoes", Method: HttpMethods.POST, Data: request));

        public ValueTask<ResponseDTO> DeleteProductAsync(Guid Id) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/delete-by-id/{Id}", Method: HttpMethods.DELETE));

        public ValueTask<ResponseDTO> GetProductByIdAsync(Guid Id) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-id/{Id}", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> GetProductByIdWithTypeAsync(Guid Id, ProductType Type) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-id/{Id}/with-type/{Type}", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> GetProductByNameAsync(string Name) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-name/{Name}", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> GetProductByNameWithTypeAsync(string Name, ProductType Type) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-name/{Name}/with-type/{Type}", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> GetProductsListAsync() => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products", Method: HttpMethods.GET));

        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateClothingRequest request) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/update-product-clothing", Method: HttpMethods.PUT, Data: request));

        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateShoesRequest request) => clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/update-product-clothing", Method: HttpMethods.PUT, Data: request));
    }
}
