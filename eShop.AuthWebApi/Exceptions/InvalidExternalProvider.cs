namespace eShop.AuthWebApi.Exceptions
{
    public class InvalidExternalProvider(string ProviderName) : Exception($"Invalid provider with name: {ProviderName}"), INotFoundException;
}
