﻿namespace eShop.Domain.Requests.Comments;

public record DeleteCommentRequest() : RequestBase
{
    public Guid CommentId { get; set; }
}