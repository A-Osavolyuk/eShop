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

        public async ValueTask<ResponseDTO> CreateProductAsync(IEnumerable<CreateProductRequest> request)
        {
            return request.First().ProductType switch 
            {
                ProductType.Clothing =>  await clientService.SendAsync(new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/create-product-clothing", Method: HttpMethods.POST, Data: request)),
                ProductType.Shoes =>  await clientService.SendAsync(new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/create-product-shoes", Method: HttpMethods.POST, Data: request)),
                ProductType.None or _ => new ResponseBuilder().Failed().WithErrorMessage("Cannot create product with product type None").Build()
            };
        }

        public async ValueTask<ResponseDTO> DeleteProductAsync(Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/delete-by-id/{Id}", Method: HttpMethods.DELETE));

        public async ValueTask<ResponseDTO> GetProductByIdAsync(Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-id/{Id}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByArticleAsync(long Article) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-article/{Article}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductByNameAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-by-name/{Name}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductsListAsync() => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-products-list", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> GetProductsByNameAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/get-products-with-name/{Name}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> UpdateProductAsync(UpdateClothingRequest request, Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/update-product-clothing/{Id}", Method: HttpMethods.PUT, Data: request));

        public async ValueTask<ResponseDTO> UpdateProductAsync(UpdateShoesRequest request, Guid Id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/update-product-clothing/{Id}", Method: HttpMethods.PUT, Data: request));

        public async ValueTask<ResponseDTO> SearchProductAsync(long Article) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/search-by-article/{Article}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> SearchProductAsync(string Name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Products/search-by-name/{Name}", Method: HttpMethods.GET));
    }
}
