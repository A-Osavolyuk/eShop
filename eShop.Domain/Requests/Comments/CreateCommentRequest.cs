﻿namespace eShop.Domain.Requests.Comments;

public record CreateCommentRequest() : RequestBase
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string CommentText { get; set; } = string.Empty;
    public List<string> Images { get; set; } =  new List<string>();
    public int Rating { get; set; }
};