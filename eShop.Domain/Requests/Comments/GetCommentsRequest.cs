using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Comments;

public record GetCommentsRequest() : RequestBase
{
    public Guid ProductId { get; set; }
}