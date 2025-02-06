using eShop.Auth.Api.Interfaces;

namespace eShop.Auth.Api.Services;

public class MessageService(IBus bus) : IMessageService
{
    private readonly IBus bus = bus;
    
    public async ValueTask SendMessageAsync(string queryName, EmailBase message)
    {
        var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
        await endpoint.Send(message);
    }

    public async ValueTask SendMessageAsync(string queryName, SmsBase message)
    {
        var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
        await endpoint.Send(message);
    }
    
    private Uri CreateQueryUri(string queryName)
    {
        const string uriBase = "rabbitmq://localhost/";

        var uri = new Uri(
            new StringBuilder(uriBase)
                .Append(queryName)
                .ToString());

        return uri;
    }
}