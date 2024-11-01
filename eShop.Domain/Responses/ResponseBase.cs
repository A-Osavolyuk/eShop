namespace eShop.Domain.Responses;

public class ResponseBase
{
    public string Message { get; set; } = string.Empty;
    public bool IsSucceeded { get; set; }
}