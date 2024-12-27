using eShop.Domain.Messages.Sms;

namespace eShop.AuthApi.Services.Interfaces;

public interface ISmsService
{
    public ValueTask SendMessageAsync<TMessage>(string queryName, TMessage message) where TMessage: SmsBase;
}