using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;
using eShop.Domain.Models;

namespace eShop.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductModel, CreateProductRequest>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<CreateProductRequest, Clothing>();
            CreateMap<CreateProductRequest, Shoes>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ShoesDTO>();
            CreateMap<Product, ClothingDTO>();
            CreateMap<Product, Shoes>();
            CreateMap<Product, Clothing>();
            CreateMap<Clothing, ClothingDTO>();
            CreateMap<Shoes, ShoesDTO>();
            CreateMap<Shoes, ProductDTO>();
            CreateMap<Clothing, ProductDTO>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
