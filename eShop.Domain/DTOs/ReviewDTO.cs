﻿namespace eShop.Domain.DTOs
{
    public class ReviewDTO
    {
        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Rating { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; } = Enumerable.Empty<CommentDto>();
    }
}
