namespace eShop.Domain.DTOs.Requests
{
    public class ConfirmChangePhoneNumberRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
