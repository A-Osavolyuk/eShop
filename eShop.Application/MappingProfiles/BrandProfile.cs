using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>();
            CreateMap<CreateBrandRequest, Brand>();
            CreateMap<UpdateBrandRequest, Brand>();
        }
    }
}
