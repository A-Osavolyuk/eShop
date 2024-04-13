using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Clothing, ClothingDTO>();
            CreateMap<Shoes, ShoesDTO>();
        }
    }
}
