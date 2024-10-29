using eShop.Domain.Common;
using eShop.Domain.Entities.Cart;
using MongoDB.Driver;

namespace eShop.CartWebApi.Queries.Favorites;

public record GetFavoritesQuery(Guid UserId) : IRequest<Result<FavoritesDto>>;

public class GetFavoritesQueryHandler(
    IMongoDatabase database,
    ILogger<GetFavoritesQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetFavoritesQuery, Result<FavoritesDto>>
{
    private readonly IMongoDatabase database = database;
    private readonly ILogger<GetFavoritesQueryHandler> logger = logger;
    private readonly IMapper mapper = mapper;

    public async Task<Result<FavoritesDto>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        var actionMessage = new ActionMessage("get favorites with ID {0}", request.UserId);
        try
        {
            logger.LogInformation("Attempting to get favorites with ID {id}.", request.UserId);
            
            var collection = database.GetCollection<FavoritesEntity>("Favorites");
            var favorites = await collection.Find(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

            if (favorites is null)
            {
                return logger.LogErrorWithException<FavoritesDto>(new NotFoundFavoritesByUserIdException(request.UserId), actionMessage);
            }
            
            logger.LogInformation("Successfully found favorites with ID {id}.", request.UserId);
            
            return mapper.Map<FavoritesDto>(favorites);
        }
        catch (Exception ex)
        {
            return logger.LogErrorWithException<FavoritesDto>(ex, actionMessage);
        }
    }
}