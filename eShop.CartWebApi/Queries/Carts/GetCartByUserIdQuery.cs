using eShop.Domain.Common;
using LanguageExt;

namespace eShop.CartWebApi.Queries.Carts
{
    public record GetCartByUserIdQuery(Guid UserId) : IRequest<Result<CartDTO>>;

    public class GetCartByUserIdQueryHandler(
        CartDbContext context, 
        ILogger<GetCartByUserIdQueryHandler> logger, 
        IMapper mapper) : IRequestHandler<GetCartByUserIdQuery, Result<CartDTO>>
    {
        private readonly CartDbContext context = context;
        private readonly ILogger<GetCartByUserIdQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<CartDTO>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find cart by user ID {0}", request.UserId);
            try
            {
                logger.LogInformation("Attempting to find cart by user ID {userId}.", request.UserId);

                var cart = await context.Carts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == request.UserId);

                if (cart.IsNull())
                {
                    return logger.LogErrorWithException<CartDTO>(new NotFoundCartByUserIdException(request.UserId), actionMessage);
                }

                logger.LogInformation("Successfully found cart by user ID {userId}", request.UserId);
                return mapper.Map<CartDTO>(cart);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<CartDTO>(ex, actionMessage);
            }
        }
    }
}
