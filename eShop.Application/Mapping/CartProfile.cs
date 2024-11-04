using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Models;

namespace eShop.Application.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartEntity, CartDto>();
            CreateMap<CartDto, CartModel>();
        }
    }
}
