using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;
using MassTransit;

namespace eShop.ReviewsWebApi.Receivers
{
    public class ProductDeletedReceiver(
        ISender sender, 
        ILogger<ProductDeletedReceiver> logger,
        ReviewDbContext dbContext)
        : IConsumer<DeleteCommentsRequest>
    {
        private readonly ISender sender = sender;
        private readonly ILogger<ProductDeletedReceiver> logger = logger;
        private readonly ReviewDbContext dbContext = dbContext;

        public async Task Consume(ConsumeContext<DeleteCommentsRequest> context)
        {
            logger.LogInformation("Got message with command to delete comments with product ID: {id}. Request ID {requestId}",
                context.Message.ProductId, context.Message.RequestId);
            
                var comments = await dbContext.Comments
                    .AsNoTracking()
                    .Where(c => c.ProductId == context.Message.ProductId)
                    .ToListAsync();

                if (comments.Any())
                {
                    dbContext.Comments.RemoveRange(comments);
                    await dbContext.SaveChangesAsync();
                }
                
                await context.RespondAsync<DeleteCommentResponse>(new DeleteCommentResponse()
                {
                    Message = "Comments were successfully deleted.",
                });
            
            logger.LogInformation($"Response was successfully sent.", context.Message.RequestId);
        }
    }
}