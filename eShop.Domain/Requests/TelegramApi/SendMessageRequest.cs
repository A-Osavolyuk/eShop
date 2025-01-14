namespace eShop.Domain.Requests.TelegramApi;

public class SendMessageRequest
{
    public long ChatId { get; set; }
    public string Message { get; set; } = string.Empty;
}