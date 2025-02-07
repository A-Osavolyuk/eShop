namespace eShop.Domain.DTOs;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int Rating { get; set; }
    public List<CommentDto> Comments { get; set; } = [];
}