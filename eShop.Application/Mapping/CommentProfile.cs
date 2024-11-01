using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;
using eShop.Domain.Requests.Comments;

namespace eShop.Application.Mapping;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentRequest, CommentEntity>();
        CreateMap<UpdateCommentRequest, CommentEntity>();
        CreateMap<CommentEntity, CommentDto>();
    }
}