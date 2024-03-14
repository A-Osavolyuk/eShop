using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class SubcategoryProfile : Profile
    {
        public SubcategoryProfile()
        {
            CreateMap<SubcategoryDto, SubcategoryEntity>();
            CreateMap<SubcategoryEntity, SubcategoryDto>();
            CreateMap<CreateUpdateSubcategoryRequestDto, SubcategoryDto>();
        }
    }
}
