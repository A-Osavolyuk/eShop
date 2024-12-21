namespace eShop.SmsSenderApi.Services;

public interface ISmsService
{
    public Task<SingleMessageResponse> SendSingleMessage(SingleMessageRequest request);
}