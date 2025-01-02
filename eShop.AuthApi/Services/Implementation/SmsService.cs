namespace eShop.AuthApi.Services.Implementation;

public class SmsService(IBus bus) : ISmsService
{
    private readonly IBus bus = bus;

    public async ValueTask SendMessageAsync<TMessage>(string queryName, TMessage message) where TMessage: SmsBase
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