using eShop.Domain.Common.Api;
using eShop.Domain.Requests.CartApi.Cart;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

internal class CartService(IHttpClientService httpClient, IConfiguration configuration) : ICartService
{
    private readonly IHttpClientService httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> GetCartAsync(Guid userId) => await httpClient.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Carts/get-cart/{userId}", Method: HttpMethods.GET));

    public async ValueTask<Response> UpdateCartAsync(UpdateCartRequest request) => await httpClient.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Carts/update-cart",
            Method: HttpMethods.PUT, Data: request));
}