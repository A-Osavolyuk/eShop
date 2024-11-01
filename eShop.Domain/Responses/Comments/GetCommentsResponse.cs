using eShop.Domain.DTOs;

namespace eShop.Domain.Responses.Comments;

public class GetCommentsResponse : ResponseBase
{
    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
}