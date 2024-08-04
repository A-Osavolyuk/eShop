using eShop.Domain.Common;

namespace eShop.CartWebApi.Receivers
{
    public class CreateCartReceiver(ISender sender, ILogger<CreateCartReceiver> logger, CartDbContext context) : IConsumer<CreateCartRequest>
    {
        private readonly ISender sender = sender;
        private readonly ILogger<CreateCartReceiver> logger = logger;
        private readonly CartDbContext dbContext = context;

        public async Task Consume(ConsumeContext<CreateCartRequest> context)
        {
            var actionMessage = new ActionMessage("create cart for user with ID {0}", context.Message.UserId);
            try
            {
                logger.LogInformation("Received command to create cart for user with ID {userId}. Request ID {requestId}", context.Message.UserId, context.Message.RequestId);

                await dbContext.Carts.AddAsync(new Cart() { UserId = context.Message.UserId });
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Command was successfully executed. Cart for user with ID {userId} was successfully created. Request ID {requestId}",
                    context.Message.UserId, context.Message.RequestId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, actionMessage, context.Message.RequestId);
            }
        }
    }
}
