using eShop.Application.Mapping;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Favorites;
using MongoDB.Driver;

namespace eShop.CartApi.Queries.Favorites;

internal sealed record GetFavoritesQuery(Guid UserId) : IRequest<Result<FavoritesDto>>;

internal sealed class GetFavoritesQueryHandler(
    IMongoDatabase database) : IRequestHandler<GetFavoritesQuery, Result<FavoritesDto>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<FavoritesDto>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<FavoritesEntity>("Favorites");
        var favorites = await collection.Find(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

        if (favorites is null)
        {
            return new(new NotFoundException($"Cannot find favorites with user ID {request.UserId}"));
        }

        return FavoritesMapper.ToFavoritesDto(favorites);
    }
}