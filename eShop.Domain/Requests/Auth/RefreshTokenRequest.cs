namespace eShop.Domain.Requests.Auth
{
    public record class RefreshTokenRequest
    {
        public string Token { get; set; } = string.Empty;
    }
}
