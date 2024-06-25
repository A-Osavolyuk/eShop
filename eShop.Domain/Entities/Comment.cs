namespace eShop.Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Review Review { get; set; } = null!;
    }
}
