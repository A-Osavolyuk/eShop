using eShop.Domain.Common.Api;
using eShop.Domain.Interfaces.Client;
using eShop.Domain.Requests.Api.Product;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class ProductService(
    IHttpClientService clientService,
    IConfiguration configuration) : IProductService
{
    private readonly IHttpClientService clientService = clientService;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> CreateProductAsync(CreateProductRequest request) =>
        await clientService.SendAsync(
            new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/create-product", Method: HttpMethods.Post, Data: request));

    public async ValueTask<Response> UpdateProductAsync(UpdateProductRequest request) =>
        await clientService.SendAsync(
            new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/update-product", Method: HttpMethods.Put, Data: request));

    public async ValueTask<Response> DeleteProductAsync(DeleteProductRequest request) =>
        await clientService.SendAsync(
            new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/delete-product", Method: HttpMethods.Delete, Data: request));

    public async ValueTask<Response> GetProductsAsync() => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/get-products",
            Method: HttpMethods.Get));

    public async ValueTask<Response> GetProductByNameAsync(string name) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/get-product-by-name/{name}",
            Method: HttpMethods.Get));

    public async ValueTask<Response> GetProductByArticleAsync(string article) => await clientService.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/get-product-by-article/{article}",
            Method: HttpMethods.Get));

    public async ValueTask<Response> GetProductByIdAsync(Guid id) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Products/get-product-by-id/{id}",
            Method: HttpMethods.Get));
}