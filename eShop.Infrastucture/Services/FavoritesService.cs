using eShop.Domain.Common.Api;
using eShop.Domain.Requests.CartApi.Favorites;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class FavoritesService(
    IHttpClientService clientService,
    IConfiguration configuration) : IFavoritesService
{
    private readonly IHttpClientService clientService = clientService;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<Response> GetFavoritesAsync(Guid userId) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Services:Gateway"]}/api/v1/Favorites/get-favorites/{userId}", Method: HttpMethods.GET));

    public async ValueTask<Response> UpdateFavoritesAsync(UpdateFavoritesRequest request) => await clientService.SendAsync(
        new Request(Url: $"{configuration["Services:Gateway"]}/api/v1/Favorites/update-favorites", Method: HttpMethods.PUT, Data: request));
}