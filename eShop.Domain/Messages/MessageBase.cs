namespace eShop.Domain.Messages
{
    public class MessageBase
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
