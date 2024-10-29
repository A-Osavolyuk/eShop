using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;
using eShop.Domain.Requests.Brand;

namespace eShop.Application.MappingProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandEntity, BrandDTO>();
            CreateMap<CreateBrandRequest, BrandEntity>();
            CreateMap<UpdateBrandRequest, BrandEntity>();
        }
    }
}
