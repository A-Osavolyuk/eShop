using eShop.Domain.Common.Api;
using eShop.Domain.Interfaces.Client;
using eShop.Domain.Requests.Api.Seller;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class SellerService(IHttpClientService httpClient, IConfiguration configuration) : ISellerService
{
    private readonly IHttpClientService httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> RegisterSellerAsync(RegisterSellerRequest request) =>
        await httpClient.SendAsync(new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Seller/register-seller", Method: HttpMethods.Post,
            Data: request));
}