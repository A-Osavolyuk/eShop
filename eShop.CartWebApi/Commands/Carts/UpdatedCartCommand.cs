using eShop.Domain.Common;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Cart;
using eShop.Domain.Responses.Cart;
using MongoDB.Driver;

namespace eShop.CartWebApi.Commands.Carts;

public record UpdatedCartCommand(UpdateCartRequest Request) : IRequest<Result<UpdateCartResponse>>;

public class UpdatedCartCommandHandler(
    ILogger<UpdatedCartCommandHandler> logger,
    IMongoDatabase database) : IRequestHandler<UpdatedCartCommand, Result<UpdateCartResponse>>
{
    private readonly ILogger<UpdatedCartCommandHandler> logger = logger;
    private readonly IMongoDatabase database = database;

    public async Task<Result<UpdateCartResponse>> Handle(UpdatedCartCommand request, CancellationToken cancellationToken)
    {
        var actionMessage = new ActionMessage("update cart with ID {0}", request.Request.CartId);
        try
        {
            logger.LogInformation("Attempting to update cart with ID {cartId}. Request ID {requestId}",
                request.Request.CartId, request.Request.RequestId);
            
            var cartCollection = database.GetCollection<CartEntity>("Carts");
            
            var cart = await cartCollection.Find(x => x.CartId == request.Request.CartId).FirstOrDefaultAsync(cancellationToken);

            if (cart is null)
            {
                return logger.LogErrorWithException<UpdateCartResponse>(new NotFoundCartException(request.Request.CartId), 
                    actionMessage, request.Request.RequestId);
            }
            else
            {
                var newCart = new CartEntity
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    ItemsCount = request.Request.ItemsCount,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = cart.CreatedAt,
                    Items = request.Request.Items
                };
                
                await cartCollection.ReplaceOneAsync(x => x.CartId ==request.Request.CartId, newCart, cancellationToken: cancellationToken);
                
                logger.LogInformation("Cart with ID {cartId} was successfully updated. Request ID {requestId}",
                    request.Request.CartId, request.Request.RequestId);
                
                return new UpdateCartResponse()
                {
                    Message = "Cart was successfully updated",
                    Succeeded = true
                };
            }
        }
        catch (Exception ex)
        {
            return logger.LogErrorWithException<UpdateCartResponse>(ex, actionMessage, request.Request.RequestId);
        }
    }
}