namespace eShop.SmsSender.Api.Services;

public interface ISmsService
{
    public Task<SingleMessageResponse> SendSingleMessage(SingleMessageRequest request);
}