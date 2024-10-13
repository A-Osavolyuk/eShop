namespace eShop.Domain.Interfaces
{
    public interface ITokenProvider
    {
        public ValueTask<string> GetTokenAsync();
        public ValueTask SetTokenAsync(string refreshToken);
        public ValueTask RemoveTokenAsync();
    }
}
