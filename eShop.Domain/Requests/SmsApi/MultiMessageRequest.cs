namespace eShop.Domain.Requests.SmsApi;

public class MultiMessageRequest
{
    public string Message { get; set; } = string.Empty;
    public List<string> PhoneNumbers { get; set; } = new List<string>();
}

