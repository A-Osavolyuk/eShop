using eShop.Domain.Common;
using eShop.Domain.Entities.Cart;
using MongoDB.Driver;

namespace eShop.CartWebApi.Queries.Carts;

public record GetCartQuery(Guid Id) : IRequest<Result<CartDto>>;

public class GetCartQueryHandler(
    IMongoDatabase database,
    ILogger<GetCartQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetCartQuery, Result<CartDto>>
{
    private readonly IMongoDatabase database = database;
    private readonly ILogger<GetCartQueryHandler> logger = logger;
    private readonly IMapper mapper = mapper;

    public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var actionMessage = new ActionMessage("get cart with ID {0}", request.Id);
        try
        {
            var collection = database.GetCollection<CartEntity>("Carts");
            var cart = await collection.Find(x => x.UserId == request.Id).FirstOrDefaultAsync(cancellationToken);

            if (cart is null)
            {
                return logger.LogInformationWithException<CartDto>(
                    new NotFoundException($"Cannot find cart with user ID {request.Id}."), actionMessage);
            }
            
            logger.LogInformation("Cart with ID {id} was successfully found.", request.Id);
            return mapper.Map<CartDto>(cart);
        }
        catch (Exception ex)
        {
            return logger.LogErrorWithException<CartDto>(ex, actionMessage);
        }
    }
}