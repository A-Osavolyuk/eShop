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
    
    public static BrandEntity ToBrandEntity(BrandDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Country = dto.Country,
        };
    }
}