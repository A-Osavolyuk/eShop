using eShop.Domain.Common.Api;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class BrandSevice(
    IHttpClientService clientService,
    IConfiguration configuration) : IBrandService
{
    private readonly IHttpClientService clientService = clientService;
    private readonly IConfiguration configuration = configuration;
    public async ValueTask<Response> GetBrandsListAsync() => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Brands", Method: HttpMethods.Get));
}