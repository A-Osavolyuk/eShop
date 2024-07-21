namespace eShop.CartWebApi.Repositories
{
    public class CartRepository(CartDbContext context, ILogger<CartRepository> logger, IMapper mapper) : ICartRepository
    {
        private readonly CartDbContext context = context;
        private readonly ILogger<CartRepository> logger = logger;
        private readonly IMapper mapper = mapper;

        public async ValueTask<Result<Unit>> AddGoodAsync(AddGoodToCartRequest request)
        {
            try
            {
                logger.LogInformation($"Trying to add good to cart.", request.RequestId);

                var cart = await context.Carts.FirstOrDefaultAsync(x => x.CartId == request.CartId);

                if (cart is not null)
                {
                    cart.Goods.Add(request.Good);

                    context.Carts.Update(cart);
                    await context.SaveChangesAsync();

                    return new(new Unit());
                }

                var notFoundCartException = new NotFoundCartException(request.CartId);
                logger.LogError($"Failed on adding good to cart with error message: {notFoundCartException.Message}.", request.RequestId);
                return new(notFoundCartException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on adding good to cart with error message: {ex.Message}.", request.RequestId);
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> CreateCartAsync(CreateCartRequest request)
        {
            try
            {
                logger.LogInformation($"Trying to create cart.", request.RequestId);

                await context.Carts.AddAsync(new Cart() { UserId = request.UserId });
                await context.SaveChangesAsync();

                logger.LogInformation("Cart was successfully created.", request.RequestId);

                return new(new Unit());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on creating cart with error message: {ex.Message}.", request.RequestId);
                return new(ex);
            }
        }

        public async ValueTask<Result<CartDTO>> GetCartByUserIdAsync(Guid UserId)
        {
            try
            {
                logger.LogInformation($"Trying to get cart by user id: {UserId}.");

                var cart = await context.Carts.AsNoTracking()
                    .AsQueryable()
                    .ProjectTo<CartDTO>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.UserId == UserId);

                if (cart is not null)
                {
                    logger.LogInformation($"Successfully found cart by user id: {UserId}.");
                    return new(cart); 
                }

                var notFoundCartByUserId = new NotFoundCartByUserIdException(UserId);
                logger.LogInformation($"Failed on getting cart by user id: {UserId} with error message: {notFoundCartByUserId}");
                return new(notFoundCartByUserId);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting cart by user id: {UserId} with error message: {ex.Message}.");
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> UpdateCartAsync(UpdateCartRequest request)
        {
            try
            {
                logger.LogInformation($"Trying to update cart with id: {request.CartId}.", request.RequestId);

                var exists = await context.Carts.AsNoTracking().AnyAsync(x => x.CartId == request.CartId && x.UserId == request.UserId);

                if (exists) 
                {
                    var cart = mapper.Map<Cart>(request);
                    context.Carts.Update(cart);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Cart with id: {request.CartId} was successfully updated.", request.RequestId);
                    return new(new Unit());
                }

                var notFoundCartException = new NotFoundCartException(request.CartId);
                logger.LogInformation($"Failed on updating cart with id: {request.CartId} with error message: {notFoundCartException.Message}");
                return new(notFoundCartException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on updating cart with id: {request.CartId} with error message: {ex.Message}.");
                return new(ex);
            }
        }
    }

    public interface ICartRepository
    {
        public ValueTask<Result<Unit>> AddGoodAsync(AddGoodToCartRequest request);
        public ValueTask<Result<Unit>> CreateCartAsync(CreateCartRequest request);
        public ValueTask<Result<CartDTO>> GetCartByUserIdAsync(Guid UserId);
        public ValueTask<Result<Unit>> UpdateCartAsync(UpdateCartRequest request);
    }
}
