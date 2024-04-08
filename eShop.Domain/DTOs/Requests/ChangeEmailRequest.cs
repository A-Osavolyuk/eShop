namespace eShop.Domain.DTOs.Requests
{
    public class ChangeEmailRequest
    {
        public string CurrentEmail { get; set; } = string.Empty;
        public string NewEmail { get; set; } = string.Empty;
    }
}
