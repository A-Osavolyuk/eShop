namespace eShop.Domain.Interfaces
{
    public interface ITokenProvider
    {
        public ValueTask<string> GetTokenAsync();
        public ValueTask SetTokenAsync(string token);
        public ValueTask RemoveTokenAsync();
    }
}
