namespace eShop.Domain.DTOs.Requests
{
    public class RequestBase
    {
        public Guid RequestId { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
