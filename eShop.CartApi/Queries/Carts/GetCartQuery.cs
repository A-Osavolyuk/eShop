using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Cart;
using MongoDB.Driver;

namespace eShop.CartApi.Queries.Carts;

internal record GetCartQuery(Guid UserId) : IRequest<Result<CartDto>>;

internal sealed class GetCartQueryHandler(
    IMongoDatabase database,
    ILogger<GetCartQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetCartQuery, Result<CartDto>>
{
    private readonly ILogger<GetCartQueryHandler> logger = logger;
    private readonly IMapper mapper = mapper;

    public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collection = database.GetCollection<CartEntity>("Carts");
            var cart = await collection.Find(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

            if (cart is null)
            {
                return new(new NotFoundException($"Cannot find cart with user ID {request.UserId}."));
            }
            
            return mapper.Map<CartDto>(cart);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}