using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Favorites;
using MongoDB.Driver;

namespace eShop.CartApi.Queries.Favorites;

public record GetFavoritesQuery(Guid UserId) : IRequest<Result<FavoritesDto>>;

public class GetFavoritesQueryHandler(
    IMongoDatabase database,
    IMapper mapper) : IRequestHandler<GetFavoritesQuery, Result<FavoritesDto>>
{
    private readonly IMongoDatabase database = database;
    private readonly IMapper mapper = mapper;

    public async Task<Result<FavoritesDto>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<FavoritesEntity>("Favorites");
            var favorites = await collection.Find(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

            if (favorites is null)
            {
                return new (new NotFoundException($"Cannot find favorites with user ID {request.UserId}"));
            }
            return mapper.Map<FavoritesDto>(favorites);
        }
        catch (Exception ex)
        {
            return new (ex);
        }
    }
}