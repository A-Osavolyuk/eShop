namespace eShop.Domain.Messages.Email;

public class EmailBase
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}