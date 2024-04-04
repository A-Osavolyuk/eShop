namespace eShop.Domain.DTOs.Requests
{
    public class ChangeUserNameRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
