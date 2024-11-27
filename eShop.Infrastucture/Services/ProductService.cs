using eShop.Domain.Requests.Product;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services
{
    public class ProductService(
        IHttpClientService clientService,
        IConfiguration configuration) : IProductService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDto> CreateProductAsync(CreateProductRequest request) =>
            await clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/create-product", Method: HttpMethods.POST, Data: request));

        public async ValueTask<ResponseDto> UpdateProductAsync(UpdateProductRequest request) =>
            await clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/update-product", Method: HttpMethods.PUT, Data: request));

        public async ValueTask<ResponseDto> DeleteProductAsync(DeleteProductRequest request) =>
            await clientService.SendAsync(
                new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/delete-product", Method: HttpMethods.DELETE, Data: request));

        public async ValueTask<ResponseDto> GetProductsAsync() => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/get-products",
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDto> GetProductByNameAsync(string name) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/get-product-by-name/{name}",
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDto> GetProductByArticleAsync(string article) => await clientService.SendAsync(
            new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Products/get-product-by-article/{article}",
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDto> GetProductByIdAsync(Guid id) => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Products/get-product-by-id/{id}",
                Method: HttpMethods.GET));
    }
}