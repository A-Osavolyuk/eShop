using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities.Cart;
using eShop.Domain.Models;

namespace eShop.Application.Mapping;

public class FavoritesProfile : Profile
{
    public FavoritesProfile()
    {
        CreateMap<FavoritesEntity, FavoritesDto>();
        CreateMap<FavoritesDto, FavoritesModel>();
    }
}