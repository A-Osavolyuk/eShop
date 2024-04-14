namespace eShop.Domain.DTOs.Requests.Auth
{
    public class ConfirmChangePhoneNumberRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
