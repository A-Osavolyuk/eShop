using eShop.Domain.Entities;

namespace eShop.Application.Mapping;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(CommentEntity entity)
    {
        return new()
        {
            UpdatedAt = entity.UpdatedAt,
            CommentId = entity.CommentId,
            UserId = entity.UserId,
            CreatedAt = entity.CreatedAt,
            Rating = entity.Rating,
            CommentText = entity.CommentText,
            Images = entity.Images,
            Username = entity.Username
        };
    }

    public static CommentModel ToCommentModel(CommentDto dto)
    {
        return new()
        {
            UpdatedAt = dto.UpdatedAt,
            CommentId = dto.CommentId,
            UserId = dto.UserId,
            CreatedAt = dto.CreatedAt,
            Rating = dto.Rating,
            CommentText = dto.CommentText,
            Images = dto.Images,
            Username = dto.Username
        };
    }

    public static CommentEntity ToCommentEntity(CreateCommentRequest request)
    {
        return new()
        {
            Username = request.Username,
            Images = request.Images,
            Rating = request.Rating,
            CommentText = request.CommentText,
            CreatedAt = DateTime.Now,
            ProductId = request.ProductId,
            UserId = request.UserId
        };
    }
    
    public static CommentEntity ToCommentEntity(UpdateCommentRequest request)
    {
        return new()
        {
            Username = request.Username,
            Images = request.Images,
            Rating = request.Rating,
            CommentText = request.CommentText,
            UpdatedAt = DateTime.Now,
            ProductId = request.ProductId,
            CommentId = request.CommentId,
            UserId = request.UserId
        };
    }
}