namespace eShop.Domain.DTOs.Requests.Cart
{
    public record class CreateCartRequest : RequestBase
    {
        public Guid UserId { get; set; }
    }
}
