using eShop.Application.Validation.Cart;
using eShop.Domain.Common;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Requests.Cart;
using eShop.Domain.Responses.Cart;
using Product = eShop.Domain.Entities.Cart.Product;

namespace eShop.CartWebApi.Commands.Carts;

public record UpdateCartCommand(UpdateCartRequest Request) : IRequest<Result<UpdateCartResponse>>;

public class UpdateCartCommandHandler(
    CartDbContext context,
    ILogger<UpdateCartCommandHandler> logger,
    IValidator<UpdateCartRequest> validator) : IRequestHandler<UpdateCartCommand, Result<UpdateCartResponse>>
{
    private readonly CartDbContext context = context;
    private readonly ILogger<UpdateCartCommandHandler> logger = logger;
    private readonly IValidator<UpdateCartRequest> validator = validator;

    public async Task<Result<UpdateCartResponse>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var actionMessage = new ActionMessage("update cart with ID {0}", request.Request.CartId);
        try
        {
            logger.LogInformation("Attempting to update cart with ID {cartID}. Request ID {requestId}",
                request.Request.CartId, request.Request.RequestId);

            var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

            if (!validationResult.IsValid)
            {
                logger.LogErrorWithException<UpdateCartResponse>(
                    new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
            }

            var cart = await context.Carts.AsNoTracking()
                .SingleOrDefaultAsync(x => x.CartId == request.Request.CartId, cancellationToken);

            if (cart is null)
            {
                return logger.LogErrorWithException<UpdateCartResponse>(
                    new NotFoundCartException(request.Request.CartId), actionMessage, request.Request.RequestId);
            }

            var cartProducts = await context.CartProducts.AsNoTracking()
                .Where(x => x.CartId == request.Request.CartId)
                .ToListAsync(cancellationToken);

            RemoveCartProducts(cartProducts, request.Request.Products);
            foreach (var product in request.Request.Products)
            {
                var cartProduct = cartProducts.SingleOrDefault(x =>
                    x.CartId == request.Request.CartId
                    && x.ProductId == product.ProductId);

                if (cartProduct is not null)
                {
                    context.CartProducts.Update(new CartProduct()
                    {
                        CartId = request.Request.CartId,
                        ProductId = product.ProductId,
                        Ammount = product.Ammount,
                        AddedAt = DateTime.UtcNow,
                    });
                }
                else
                {
                    await context.CartProducts.AddAsync(new CartProduct()
                    {
                        CartId = request.Request.CartId,
                        ProductId = product.ProductId,
                        Ammount = product.Ammount,
                        AddedAt = DateTime.UtcNow,
                    }, cancellationToken);
                }
            }
            
            await context.SaveChangesAsync(cancellationToken);
            
            logger.LogInformation("Successfully updated cart with ID {cartID}. Request ID {requestId}", 
                request.Request.CartId, request.Request.RequestId);

            return new UpdateCartResponse()
            {
                Succeeded = true,
                Message = "Successfully updated cart"
            };
        }
        catch (Exception ex)
        {
            return logger.LogErrorWithException<UpdateCartResponse>(ex, actionMessage, request.Request.RequestId);
        }
    }

    private void RemoveCartProducts(List<CartProduct> cartProducts, List<Product> products)
    {
        if (cartProducts.Any())
        {
            var cartProductsToSave = new List<CartProduct>();
            foreach (var cartProduct in cartProducts)
            {
                foreach (var product in products)
                {
                    if (product.ProductId == cartProduct.ProductId)
                    {
                        cartProductsToSave.Add(cartProduct);
                    }
                }
            }

            var cartProductsToRemove = cartProducts.Except(cartProductsToSave).ToList();
            context.CartProducts.RemoveRange(cartProductsToRemove);
        }
    }
}