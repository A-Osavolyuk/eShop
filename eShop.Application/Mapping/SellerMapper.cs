using eShop.Domain.DTOs.Api.Product;
using eShop.Domain.Entities.Api.Product;

namespace eShop.Application.Mapping;

public static class SellerMapper
{
    public static SellerDto ToSellerDto(SellerEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Article = entity.Article,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            UserId = entity.UserId,
        };
    }
    
    public static SellerEntity ToSellerDto(SellerDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Article = dto.Article,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            UserId = dto.UserId,
        };
    }
}