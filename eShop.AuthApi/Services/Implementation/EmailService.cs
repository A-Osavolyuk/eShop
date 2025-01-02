namespace eShop.AuthApi.Services.Implementation;

internal sealed class EmailService(IBus bus) : IEmailService
{
    private readonly IBus bus = bus;
    
    private Uri CreateQueryUri(string queryName)
    {
        const string uriBase = "rabbitmq://localhost/";

        var uri = new Uri(
            new StringBuilder(uriBase)
                .Append(queryName)
                .ToString());

        return uri;
    }

    public async ValueTask SendMessageAsync<TMessage>(string queryName, TMessage message) where TMessage : EmailBase
    {
        var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
        await endpoint.Send(message);
    }
}