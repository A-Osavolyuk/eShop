using eShop.Domain.Common;

namespace eShop.CartWebApi.Commands.Carts
{
    public record UpdateCartCommand(UpdateCartRequest Request) : IRequest<Result<CartDTO>>;

    public class UpdateCartCommandHandler(
        CartDbContext context,
        ILogger<UpdateCartCommandHandler> logger,
        IMapper mapper,
        IValidator<UpdateCartRequest> validator) : IRequestHandler<UpdateCartCommand, Result<CartDTO>>
    {
        private readonly CartDbContext context = context;
        private readonly ILogger<UpdateCartCommandHandler> logger = logger;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<UpdateCartRequest> validator = validator;

        public async Task<Result<CartDTO>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("update cart with ID {0}", request.Request.CartId);
            try
            {
                logger.LogInformation("Attempting to update cart with ID {cartId}. Request ID {requestId}", request.Request.CartId, request.Request.RequestId);

                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<CartDTO>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var cart = await context.Carts.AsNoTracking().FirstOrDefaultAsync(x => x.CartId == request.Request.CartId);

                if (cart is null)
                {
                    return logger.LogErrorWithException<CartDTO>(new NotFoundCartException(request.Request.CartId), actionMessage, request.Request.RequestId);
                }

                var removedGoods = cart.Goods.Where(c => !request.Request.Goods.Any(x => x.Id == c.Id)).ToList();
                context.Goods.RemoveRange(removedGoods);

                foreach (var good in cart.Goods)
                {
                    var existingGood = cart.Goods.FirstOrDefault(x => x.Id == good.Id);

                    if (existingGood is null)
                    {
                        await context.Goods.AddAsync(good);
                    }
                    else
                    {
                        context.Goods.Update(existingGood);
                    }
                }

                var updatedCart = mapper.Map<Cart>(request.Request);
                context.Carts.Update(updatedCart);
                await context.SaveChangesAsync();

                logger.LogInformation("Successfully updated cart with ID {cartId}. Request ID {requestId}", request.Request.CartId, request.Request.RequestId);
                return new(mapper.Map<CartDTO>(updatedCart));
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<CartDTO>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
