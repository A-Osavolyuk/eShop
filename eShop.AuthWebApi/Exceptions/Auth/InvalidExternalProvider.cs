namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class InvalidExternalProvider(string ProviderName) : Exception($"Invalid provider with name: {ProviderName}"), INotFoundException;
}
