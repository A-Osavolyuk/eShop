using eShop.Domain.Common.Api;
using eShop.Domain.Interfaces.Client;
using eShop.Domain.Requests.Api.Favorites;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class FavoritesService(
    IHttpClientService clientService,
    IConfiguration configuration) : IFavoritesService
{
    private readonly IHttpClientService clientService = clientService;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> GetFavoritesAsync(Guid userId) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Favorites/get-favorites/{userId}", Method: HttpMethods.Get));

    public async ValueTask<Response> UpdateFavoritesAsync(UpdateFavoritesRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Favorites/update-favorites", Method: HttpMethods.Put, Data: request));
}