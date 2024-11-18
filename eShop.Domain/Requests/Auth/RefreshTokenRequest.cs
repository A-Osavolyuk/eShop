namespace eShop.Domain.Requests.Auth
{
    public record class RefreshTokenRequest : RequestBase
    {
        public string Token { get; set; } = string.Empty;
    }
}
