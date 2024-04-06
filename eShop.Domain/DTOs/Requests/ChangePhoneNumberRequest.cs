namespace eShop.Domain.DTOs.Requests
{
    public class ChangePhoneNumberRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
