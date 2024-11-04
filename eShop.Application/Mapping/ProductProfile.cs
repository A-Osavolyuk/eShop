using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;
using eShop.Domain.Models;
using eShop.Domain.Requests.Product;

namespace eShop.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProduct, CreateProductRequest>();
            CreateMap<CreateProductRequest, ProductEntity>();
            CreateMap<CreateProductRequest, ClothingEntity>();
            CreateMap<CreateProductRequest, ShoesEntity>();
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<ProductEntity, ShoesDTO>();
            CreateMap<ProductEntity, ClothingDTO>();
            CreateMap<ProductEntity, ShoesEntity>();
            CreateMap<ProductEntity, ClothingEntity>();
            CreateMap<ClothingEntity, ClothingDTO>();
            CreateMap<ShoesEntity, ShoesDTO>();
            CreateMap<ShoesEntity, ProductDto>();
            CreateMap<ClothingEntity, ProductDto>();
            CreateMap<ProductEntity, ProductDto>();
        }
    }
}
