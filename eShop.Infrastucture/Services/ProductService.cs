using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
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

        public async ValueTask<ResponseDTO> CreateProductAsync(List<CreateProductRequest> request) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products", Method: HttpMethods.POST, Data: request));

        public async ValueTask<ResponseDTO> DeleteProductAsync(DeleteProductRequest request) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products", Data: request, Method: HttpMethods.DELETE));

        public async ValueTask<ResponseDTO> GetProductByIdAsync(Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/{Id}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByArticleAsync(long Article) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/{Article}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByNameAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/{Name}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductsListAsync() => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductsByNameAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/get-products-with-name/{Name}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> UpdateProductAsync(UpdateProductRequest request, Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/{Id}", Method: HttpMethods.PUT, Data: request));

        public async ValueTask<ResponseDTO> SearchProductAsync(long Article) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/search-by-article/{Article}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> SearchProductAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/search-by-name/{Name}", Method: HttpMethods.GET));
    }
}
