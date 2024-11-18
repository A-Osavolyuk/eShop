namespace eShop.Domain.Requests.Auth
{
    public record class ChangeEmailRequest : RequestBase 
    {
        public string CurrentEmail { get; set; } = string.Empty;
        public string NewEmail { get; set; } = string.Empty;
    }
}
