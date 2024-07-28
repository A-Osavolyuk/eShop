namespace eShop.Domain.DTOs.Requests
{
    public record class RequestBase
    {
        public Guid RequestId { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
