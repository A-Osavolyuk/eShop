using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Review;
using eShop.Domain.Entities;

namespace eShop.Application.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewRequest, Review>();
            CreateMap<Review, ReviewDTO>();
        }
    }
}
