namespace eShop.Domain.Requests.ReviewApi.Comments;

public record DeleteCommentRequest()
{
    public Guid CommentId { get; set; }
}