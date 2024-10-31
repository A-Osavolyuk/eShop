using MassTransit;

namespace eShop.ReviewsWebApi.Receivers
{
    public class ProductDeletedReceiver(ISender sender, ILogger<ProductDeletedReceiver> logger)
        : IConsumer<DeleteReviewsRequest>
    {
        private readonly ISender sender = sender;
        private readonly ILogger<ProductDeletedReceiver> logger = logger;

        public async Task Consume(ConsumeContext<DeleteReviewsRequest> context)
        {
            logger.LogInformation($"Got message with command to delete reviews with product id: {context.Message.Id}",
                context.Message.RequestId);

            //TODO: Delete reviews with comments logic on deleting products

            logger.LogInformation($"Response was successfully sent.", context.Message.RequestId);
        }
    }
}