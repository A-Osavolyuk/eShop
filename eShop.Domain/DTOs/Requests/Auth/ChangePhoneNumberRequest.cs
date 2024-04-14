namespace eShop.Domain.DTOs.Requests.Auth
{
    public class ChangePhoneNumberRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
