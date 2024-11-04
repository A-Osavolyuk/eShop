using AutoMapper;
using eShop.Domain.Entities.Product;
using eShop.Domain.Requests.Product;

namespace eShop.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequest, ProductEntity>();
            CreateMap<CreateProductRequest, ShoesEntity>();
            CreateMap<CreateProductRequest, ClothingEntity>();
        }
    }
}
