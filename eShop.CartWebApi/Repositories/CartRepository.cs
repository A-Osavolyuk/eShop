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

                return cart is not null ? new(cart) : new(new NotFoundCartByUserIdException(UserId));
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Failed on getting cart by user id: {UserId} with error message: {ex.Message}.");
                return new(ex);
            }
        }
    }

    public interface ICartRepository
    {
        public ValueTask<Result<Unit>> AddGoodAsync(AddGoodToCartRequest request);
        public ValueTask<Result<Unit>> CreateCartAsync(CreateCartRequest request);
        public ValueTask<Result<CartDTO>> GetCartByUserIdAsync(Guid UserId);
    }
}
