namespace eShop.Domain.Requests.ReviewApi.Comments;

public record class DeleteCommentsRequest
{
    public Guid ProductId { get; set; }
}