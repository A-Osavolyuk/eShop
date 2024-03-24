namespace eShop.Domain.DTOs.Requests
{
    public class SendResetPasswordEmailRequest
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
