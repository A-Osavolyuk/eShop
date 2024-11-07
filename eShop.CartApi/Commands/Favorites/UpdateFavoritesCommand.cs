using eShop.Domain.Common;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Favorites;
using eShop.Domain.Responses.Cart;
using MongoDB.Driver;

namespace eShop.CartApi.Commands.Favorites;

public record UpdateFavoritesCommand(UpdateFavoritesRequest Request) : IRequest<Result<UpdateFavoritesResponse>>;

public class UpdstateFavoritesCommandHandler(
    IMongoDatabase database) : IRequestHandler<UpdateFavoritesCommand, Result<UpdateFavoritesResponse>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<UpdateFavoritesResponse>> Handle(UpdateFavoritesCommand request, CancellationToken cancellationToken)
    {
        var actionMessage = new ActionMessage("update favorites with ID {0}", request.Request.FavoritesId);
        try
        {
            var collection = database.GetCollection<FavoritesEntity>("Favorites");
            var favorites = await collection.Find(x => x.FavoritesId == request.Request.FavoritesId).FirstOrDefaultAsync(cancellationToken);

            if (favorites is null)
            {
                return new(new NotFoundException($"Cannot find favorites with ID {request.Request.FavoritesId}."));
            }

            var newFavorites = new FavoritesEntity()
            {
                CreatedAt = favorites.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                FavoritesId = favorites.FavoritesId,
                ItemsCount = request.Request.ItemsCount,
                UserId = favorites.UserId,
                Items = request.Request.Items
            };
            
            await collection.ReplaceOneAsync(x => x.FavoritesId == request.Request.FavoritesId, newFavorites, cancellationToken: cancellationToken);

            return new UpdateFavoritesResponse()
            {
                Message = "Favorites successfully updated"
            };
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}