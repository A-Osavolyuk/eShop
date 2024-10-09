namespace eShop.Domain.DTOs.Requests
{
    public record class RequestBase
    {
        public RequestBase()
        {
            RequestId = Guid.NewGuid();
            RequestedAt = DateTime.UtcNow;
        }

        public Guid RequestId { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
