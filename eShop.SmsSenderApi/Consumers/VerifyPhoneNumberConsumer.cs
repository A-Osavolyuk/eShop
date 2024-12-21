namespace eShop.SmsSenderApi.Consumers;

public class VerifyPhoneNumberConsumer(ISmsService smsService) : IConsumer<SingleMessageRequest>
{
    private readonly ISmsService smsService = smsService;

    public async Task Consume(ConsumeContext<SingleMessageRequest> context)
    {
        var request = context.Message;
        var response = await smsService.SendSingleMessage(request);
        await context.RespondAsync(response);
    }
}