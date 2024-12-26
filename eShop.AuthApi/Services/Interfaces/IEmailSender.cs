namespace eShop.AuthApi.Services.Interfaces;

internal interface IEmailSender
{
    public ValueTask SendMessageAsync<TMessage>(string queryName, TMessage message) where TMessage : MessageBase;
}