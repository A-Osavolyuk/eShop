namespace eShop.Domain.DTOs.Requests.Review
{
    public record class DeleteReviewsRequest : RequestBase
    {
        public Guid Id { get; set; }
    }
}
