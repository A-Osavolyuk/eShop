namespace eShop.Domain.DTOs.Requests.Auth
{
    public class ConfirmEmailRequest
    {
        public string Token { get; set; } = string.Empty;
    }
}
