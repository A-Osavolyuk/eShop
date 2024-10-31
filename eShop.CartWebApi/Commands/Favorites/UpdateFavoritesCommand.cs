using eShop.Domain.Common;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Cart;
using eShop.Domain.Responses.Cart;
using MongoDB.Driver;

namespace eShop.CartWebApi.Commands.Favorites;

public record UpdateFavoritesCommand(UpdateFavoritesRequest Request) : IRequest<Result<UpdateFavoritesResponse>>;

public class UpdstateFavoritesCommandHandler(
    IMongoDatabase database,
    ILogger<UpdstateFavoritesCommandHandler> logger) : IRequestHandler<UpdateFavoritesCommand, Result<UpdateFavoritesResponse>>
{
    private readonly IMongoDatabase database = database;
    private readonly ILogger<UpdstateFavoritesCommandHandler> logger = logger;

    public async Task<Result<UpdateFavoritesResponse>> Handle(UpdateFavoritesCommand request, CancellationToken cancellationToken)
    {
        var actionMessage = new ActionMessage("update favorites with ID {0}", request.Request.FavoritesId);
        try
        {
            logger.LogInformation("Attempting to update favorites with ID {favoritesId}. Request ID {requestId}", 
                request.Request.FavoritesId, request.Request.RequestId);
            
            var collection = database.GetCollection<FavoritesEntity>("Favorites");
            var favorites = await collection.Find(x => x.FavoritesId == request.Request.FavoritesId).FirstOrDefaultAsync(cancellationToken);

            if (favorites is null)
            {
                return logger.LogInformationWithException<UpdateFavoritesResponse>(
                    new NotFoundException($"Cannot find favorites with ID {request.Request.FavoritesId}."), 
                    actionMessage, request.Request.RequestId);
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
            
            logger.LogInformation("Favorites with ID {id} was successfully updated. Request ID {requestId}.}", 
                request.Request.FavoritesId, request.Request.RequestId);

            return new UpdateFavoritesResponse()
            {
                Succeeded = true,
                Message = "Favorites successfully updated"
            };
        }
        catch (Exception ex)
        {
            return logger.LogErrorWithException<UpdateFavoritesResponse>(ex, actionMessage, request.Request.RequestId);
        }
    }
}