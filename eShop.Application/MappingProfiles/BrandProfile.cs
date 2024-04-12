using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>();
        }
    }
}
