namespace eShop.Domain.DTOs.Requests.Auth
{
    public record class RefreshTokenRequest : RequestBase
    {
        public string Token { get; set; } = string.Empty;
    }
}
