namespace eShop.Domain.DTOs.Requests.Auth
{
    public record class ConfirmChangeEmailRequest : RequestBase
    {
        public string CurrentEmail { get; set; } = string.Empty;
        public string NewEmail { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
