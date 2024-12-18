namespace eShop.Domain.Requests.Comments;

public record DeleteCommentRequest()
{
    public Guid CommentId { get; set; }
}