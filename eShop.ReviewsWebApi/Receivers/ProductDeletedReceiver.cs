using MassTransit;

namespace eShop.ReviewsWebApi.Receivers
{
    public class ProductDeletedReceiver(ISender sender, ILogger<ProductDeletedReceiver> logger) : IConsumer<DeleteReviewsRequest>
    {
        private readonly ISender sender = sender;
        private readonly ILogger<ProductDeletedReceiver> logger = logger;

        public async Task Consume(ConsumeContext<DeleteReviewsRequest> context)
            {
            logger.LogInformation($"Got message with command to delete reviews with product id: {context.Message.Id}", context.Message.RequestId);

            var result = await sender.Send(new DeleteReviewsWithProductIdCommand(context.Message.Id));

            logger.LogInformation($"Command was successfully executed.", context.Message.RequestId);

            var response = result.Match(
                s => new ResponseBuilder().Succeeded().WithResultMessage($"Reviews with product id: {context.Message.Id} were successfully deleted.").Build(),
                f => new ResponseBuilder().Failed().WithResultMessage(f.Message).Build());

            await context.RespondAsync(response);

            logger.LogInformation($"Response was successfully sent.", context.Message.RequestId);
        }
    }
}
