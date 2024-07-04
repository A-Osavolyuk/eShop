using eShop.Domain.Messages;
using MassTransit;

namespace eShop.ReviewsWebApi.Receivers
{
    public class ProductDeletedReceiver(ISender sender, ILogger<ProductDeletedMessage> logger) : IConsumer<ProductDeletedMessage>
    {
        private readonly ISender sender = sender;
        private readonly ILogger<ProductDeletedMessage> logger = logger;

        public async Task Consume(ConsumeContext<ProductDeletedMessage> context)
            {
            logger.LogInformation($"Got message with command to delete reviews with product id: {context.Message.Id}");

            var result = await sender.Send(new DeleteReviewsWithProductIdCommand(context.Message.Id));

            logger.LogInformation($"Command was successfully executed");

            var response = result.Match(
                s => new ReviewsDeletedMessage() { IsSucceeded = true, Status = $"Reviews with product id: {context.Message.Id} were successfully deleted" },
                f => new ReviewsDeletedMessage() { IsSucceeded = false, Status = f.Message });

            await context.RespondAsync(response);

            logger.LogInformation($"Response was successfully sent");
        }
    }
}
