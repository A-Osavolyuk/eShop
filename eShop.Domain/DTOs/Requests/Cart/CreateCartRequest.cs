namespace eShop.Domain.DTOs.Requests.Cart
{
    public class CreateCartRequest : RequestBase
    {
        public Guid UserId { get; set; }
    }
}
