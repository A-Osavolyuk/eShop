using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CreateUpdateCategoryRequest, CategoryEntity>();
        }
    }
}
