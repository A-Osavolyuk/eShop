namespace eShop.Domain.DTOs.Requests.Auth
{
    public record class ChangePhoneNumberRequest : RequestBase
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
