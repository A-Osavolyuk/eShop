namespace eShop.AuthApi.Services.Implementation;

internal sealed class EmailSender(IBus bus) : IEmailSender
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

    public async ValueTask SendMessageAsync<TMessage>(string queryName, TMessage message) where TMessage : MessageBase
    {
        var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
        await endpoint.Send(message);
    }
}