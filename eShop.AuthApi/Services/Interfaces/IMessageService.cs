namespace eShop.AuthApi.Services.Interfaces;

public interface IMessageService
{
    public ValueTask SendMessageAsync(string queryName, EmailBase message);
    public ValueTask SendMessageAsync(string queryName, SmsBase message);
}