using eShop.Domain.DTOs;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Domain.Requests.Cart;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    internal class CartService(IHttpClientService httpClient, IConfiguration configuration) : ICartService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> GetCartAsync(GetCartRequest request) => await httpClient.SendAsync(
            new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/get-cart", Data: request,
                Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest request) => await httpClient.SendAsync(
            new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/update-cart", Data: request,
                Method: HttpMethods.PUT));
    }
}