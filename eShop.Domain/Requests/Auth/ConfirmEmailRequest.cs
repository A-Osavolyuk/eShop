namespace eShop.Domain.Requests.Auth
{
    public record class ConfirmEmailRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
