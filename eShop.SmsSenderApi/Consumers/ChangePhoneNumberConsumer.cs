﻿namespace eShop.SmsSenderApi.Consumers;

public class ChangePhoneNumberConsumer(
    ISmsService smsService) : IConsumer<ChangePhoneNumberMessage>
{
    private readonly ISmsService smsService = smsService;
    
    public async Task Consume(ConsumeContext<ChangePhoneNumberMessage> context)
    {
        var request = context.Message;
        var response = await smsService.SendSingleMessage(new SingleMessageRequest()
        {
            Message = $"Phone number change code: {request.Code}",
            PhoneNumber = request.Code
        });
        await context.RespondAsync(response);
    }
}