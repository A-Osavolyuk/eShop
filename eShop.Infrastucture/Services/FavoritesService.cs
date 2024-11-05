using eShop.Domain.DTOs;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Domain.Requests.Favorites;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services;

public class FavoritesService(
    IHttpClientService clientService,
    IConfiguration configuration) : IFavoritesService
{
    private readonly IHttpClientService clientService = clientService;
    private readonly IConfiguration configuration = configuration;

    public async ValueTask<ResponseDTO> GetFavoritesAsync(Guid userId) => await clientService.SendAsync(
        new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Favorites/get-favorites/{userId}", Method: HttpMethods.GET));

    public async ValueTask<ResponseDTO> UpdateFavoritesAsync(UpdateFavoritesRequest request) => await clientService.SendAsync(
        new RequestDto(Url: $"{configuration["Services:Gateway"]}/api/v1/Favorites/update-favorites", Data:request, Method: HttpMethods.PUT));
}