using eShop.Domain.DTOs;
using eShop.Domain.Entities.Product;
using eShop.Domain.Requests.Brand;

namespace eShop.Application.Mapping;

public static class BrandMapper
{
    public static BrandEntity ToBrandEntity(CreateBrandRequest request)
    {
        return new()
        {
            Name = request.Name,
            Country = request.Country,
        };
    }

    public static BrandEntity ToBrandEntity(UpdateBrandRequest request)
    {
        return new()
        {
            Name = request.Name,
            Country = request.Country,
        };
    }

    public static BrandDto ToBrandDto(BrandEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Country = entity.Country,
        };
    }
}