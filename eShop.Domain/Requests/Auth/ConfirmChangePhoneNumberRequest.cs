namespace eShop.Domain.Requests.Auth
{
    public record class ConfirmChangePhoneNumberRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
