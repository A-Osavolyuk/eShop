using Azure.Core;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
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

        public async ValueTask<ResponseDTO> AddGoodAsync(AddGoodToCartRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/add-good-to-cart", Data: request, Method: HttpMethods.POST));

        public async ValueTask<ResponseDTO> CreateCartAsync(CreateCartRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/create-cart", Data: request, Method: HttpMethods.POST));

        public async ValueTask<ResponseDTO> GetCartByUserIdAsync(Guid UserId) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/get-cart/{UserId}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Carts/update-cart", Data: request, Method: HttpMethods.POST));
    }
}
