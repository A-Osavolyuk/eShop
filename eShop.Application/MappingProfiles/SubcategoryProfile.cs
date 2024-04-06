using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class SubcategoryProfile : Profile
    {
        public SubcategoryProfile()
        {
            CreateMap<SubcategoryDto, SubcategoryEntity>();
            CreateMap<SubcategoryEntity, SubcategoryDto>();
            CreateMap<CreateUpdateSubcategoryRequest, SubcategoryDto>();
        }
    }
}
