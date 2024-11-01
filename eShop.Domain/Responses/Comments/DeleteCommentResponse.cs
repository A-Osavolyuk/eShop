namespace eShop.Domain.Responses.Comments;

public class DeleteCommentResponse
{
    public string Message { get; set; } = string.Empty;
    public bool IsSucceeded { get; set; }
}