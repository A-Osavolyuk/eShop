namespace eShop.Domain.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<string> Images { get; set; } =  new List<string>();
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}