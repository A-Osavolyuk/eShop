using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;
using System.Data;

namespace eShop.Application.MappingProfiles
{
    public class SubcategoryProfile : Profile
    {
        public SubcategoryProfile()
        {
            CreateMap<SubcategoryDto, SubcategoryEntity>();
        }
    }
}
