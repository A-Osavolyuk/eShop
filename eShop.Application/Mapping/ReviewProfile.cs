using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;
using eShop.Domain.Requests.Review;

namespace eShop.Application.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewRequest, ReviewEntity>();
            CreateMap<UpdateReviewRequest, ReviewEntity>();
            CreateMap<ReviewEntity, ReviewDto>();
        }
    }
}
