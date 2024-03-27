using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, ProductEntity>();
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<CreateUpdateProductRequest, ProductDto>();
        }
    }
}
