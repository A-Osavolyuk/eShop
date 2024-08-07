using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Cart;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    internal class CartService(IHttpClientService httpClient, IConfiguration configuration) : ICartService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> GetCartByUserIdAsync(Guid Id) => await httpClient.SendAsync(new(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/get-cart-by-user-id/{Id}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest updateCartRequest) => await httpClient.SendAsync(new(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/update-cart", Data: updateCartRequest, Method: HttpMethods.PUT));
    }
}
