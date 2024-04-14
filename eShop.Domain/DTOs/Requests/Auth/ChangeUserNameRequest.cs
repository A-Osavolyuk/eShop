namespace eShop.Domain.DTOs.Requests.Auth
{
    public class ChangeUserNameRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
