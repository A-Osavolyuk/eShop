using AutoMapper;
using eShop.Domain.Common;
using MassTransit;
using eShop.Application.Extensions;
using eShop.Domain.Requests.Product;
using eShop.Domain.Responses.Product;

namespace eShop.ProductWebApi.Receivers;

public class ProductExistsReceiver(
    ProductDbContext dbContext,
    ILogger<ProductExistsReceiver> logger,
    IMapper mapper) : IConsumer<ProductExistsRequest>
{
    private readonly ProductDbContext dbContext = dbContext;
    private readonly ILogger<ProductExistsReceiver> logger = logger;
    private readonly IMapper mapper = mapper;

    public async Task Consume(ConsumeContext<ProductExistsRequest> context)
    {
        var actionMessage = new ActionMessage("check if product with ID {0} exists", context.Message.ProductId);
        try
        {
            logger.LogInformation("Received command to check if product with ID {productId} exists.",
                context.Message.ProductId);

            var product = await dbContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == context.Message.ProductId);

            if (product is null)
            {
                await context.RespondAsync(new ProductExistsResponse()
                {
                    Message = $"Cannot find product with ID {context.Message.ProductId}.",
                    Succeeded = false
                });
            }
            else
            {
                await context.RespondAsync(new ProductExistsResponse()
                {
                    Message = $"Successfully found product with ID {context.Message.ProductId}.",
                    Succeeded = true,
                });
            }
            
            logger.LogInformation("Command was successfully executed. Request ID {requestID}", context.Message.RequestId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, actionMessage, context.Message.RequestId);
        }
    }
}