namespace eShop.Domain.DTOs.Requests
{
    public class ChangePhoneNumberRequest
    {
        public string CurrentPhoneNumber { get; set; } = string.Empty;
        public string NewPhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
