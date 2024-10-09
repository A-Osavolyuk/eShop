namespace eShop.Domain.DTOs.Responses.Auth
{
    public class ConfirmChangePhoneNumberResponse
    {
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
