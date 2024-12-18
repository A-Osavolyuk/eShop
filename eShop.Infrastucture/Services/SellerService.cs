using eShop.Domain.Common.Api;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class SellerService(IHttpClientService httpClient, IConfiguration configuration) : ISellerService
{
    private readonly IHttpClientService httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> RegisterSellerAsync(RegisterSellerRequest request) =>
        await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Seller/register-seller", Method: HttpMethods.POST,
            Data: request));
}