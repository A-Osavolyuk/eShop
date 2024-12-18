using eShop.Domain.DTOs.ReviewApi;

namespace eShop.Domain.Responses.CartApi.Comments;

public class GetCommentsResponse : ResponseBase
{
    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
}