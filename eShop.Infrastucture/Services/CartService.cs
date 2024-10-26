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
    }
}
