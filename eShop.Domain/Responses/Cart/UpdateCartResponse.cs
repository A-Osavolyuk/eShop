namespace eShop.Domain.Responses.Cart;

public class UpdateCartResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
}