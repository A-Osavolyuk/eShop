namespace eShop.CartWebApi.Receivers
{
    public class CreateCartReceiver(ISender sender, ILogger<CreateCartReceiver> logger) : IConsumer<CreateCartRequest>
    {
        private readonly ISender sender = sender;
        private readonly ILogger<CreateCartReceiver> logger = logger;

        public async Task Consume(ConsumeContext<CreateCartRequest> context)
        {
            logger.LogInformation($"Received message with command to create cart for user with id: {context.Message.UserId}.", context.Message.RequestId);

            var result = await sender.Send(new CreateCartCommand(context.Message));

            logger.LogInformation($"Command was executed.", context.Message.RequestId);

            var response = result.Match(
               s => new ResponseBuilder().Succeeded().WithResultMessage("Cart was successfully created.").Build(),
               f => new ResponseBuilder().Succeeded().WithErrorMessage(f.Message).Build());

            logger.LogInformation("Response was successfully sent.", context.Message.RequestId);
        }
    }
}
