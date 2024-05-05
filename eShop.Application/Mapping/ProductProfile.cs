using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ShoesDTO>();
            CreateMap<Product, ClothingDTO>();
            CreateMap<Product, Shoes>();
            CreateMap<Product, Clothing>();
            CreateMap<Clothing, ClothingDTO>();
            CreateMap<Shoes, ShoesDTO>();
            CreateMap<ProductRequestBase, Product>();
            CreateMap<CreateShoesRequest, Shoes>();
            CreateMap<CreateClothingRequest, Clothing>();
            CreateMap<UpdateShoesRequest, Shoes>();
            CreateMap<UpdateClothingRequest, Clothing>();
        }
    }
}
