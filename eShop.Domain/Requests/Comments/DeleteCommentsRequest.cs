namespace eShop.Domain.Requests.Comments
{
    public record class DeleteCommentsRequest
    {
        public Guid ProductId { get; set; }
    }
}
