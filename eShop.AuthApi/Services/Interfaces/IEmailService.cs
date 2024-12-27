using eShop.Domain.Messages.Email;

namespace eShop.AuthApi.Services.Interfaces;

internal interface IEmailService
{
    public ValueTask SendMessageAsync<TMessage>(string queryName, TMessage message) where TMessage : EmailBase;
}