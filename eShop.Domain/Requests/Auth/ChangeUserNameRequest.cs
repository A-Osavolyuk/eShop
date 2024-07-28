namespace eShop.Domain.DTOs.Requests.Auth
{
    public record class ChangeUserNameRequest : RequestBase
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
