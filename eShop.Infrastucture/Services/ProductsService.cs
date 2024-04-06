using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class ProductsService(
        IHttpClientService clientService,
        IConfiguration configuration) : IProductsService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDto> CreateProductAsync(ProductDto Product)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products", Data: Product, Method: ApiMethod.POST));
        }

        public async ValueTask<ResponseDto> DeleteProductByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/{Id}", Method: ApiMethod.DELETE));
        }

        public async ValueTask<ResponseDto> GetAllProductsAsync()
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetProductByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/{Id}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetProductByNameAsync(string Name)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/{Name}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> UpdateProductAsync(ProductDto Product, Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/{Id}", Data: Product, Method: ApiMethod.PUT));
        }
    }
}
