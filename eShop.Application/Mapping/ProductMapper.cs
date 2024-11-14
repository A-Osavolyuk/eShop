using eShop.Domain.DTOs.Products;
using eShop.Domain.Entities.Product;
using eShop.Domain.Requests.Product;

namespace eShop.Application.Mapping;

public static class ProductMapper
{
    #region Create request to entities

    public static ClothingEntity ToClothingEntity(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ClothingEntity()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
            Currency = request.Currency,
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = new()
            {
                Name = request.Brand.Name,
                Country = request.Brand.Country,
                Id = request.Brand.Id,
            },
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size.ToHashSet(),
        };
    }
    
    public static ShoesEntity ToShoesEntity(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ShoesEntity()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
            Currency = request.Currency,
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = new()
            {
                Name = request.Brand.Name,
                Country = request.Brand.Country,
                Id = request.Brand.Id,
            },
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size.ToHashSet(),
        };
    }
    
    public static ProductEntity ToProductEntity(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ProductEntity()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
            Currency = request.Currency,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = new()
            {
                Name = request.Brand.Name,
                Country = request.Brand.Country,
                Id = request.Brand.Id,
            },
            Description = request.Description,
            Images = request.Images,
        };
    }

    #endregion
    
    #region Update request to entities
    
    public static ClothingEntity ToClothingEntity(UpdateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ClothingEntity()
        {
            Name = request.Name,
            Price = request.Price,
            Currency = request.Currency,
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = new()
            {
                Name = request.Brand.Name,
                Country = request.Brand.Country,
                Id = request.Brand.Id,
            },
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size,
        };
    }
    
    public static ShoesEntity ToShoesEntity(UpdateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ShoesEntity()
        {
            Name = request.Name,
            Price = request.Price,
            Currency = request.Currency,
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = new()
            {
                Name = request.Brand.Name,
                Country = request.Brand.Country,
                Id = request.Brand.Id,
            },
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size,
        };
    }
    
    public static ProductEntity ToProductEntity(UpdateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ProductEntity()
        {
            Name = request.Name,
            Price = request.Price,
            Currency = request.Currency,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = new()
            {
                Name = request.Brand.Name,
                Country = request.Brand.Country,
                Id = request.Brand.Id,
            },
            Description = request.Description,
            Images = request.Images,
        };
    }
    
    #endregion

    #region Entities to DTOs

    public static ProductDto ToProductDto(ProductEntity entity)
    {
        return new ProductDto()
        {
            Id = entity.Id,
            Article = entity.Article,
            Brand = new()
            {
                Name = entity.Brand.Name,
                Country = entity.Brand.Country,
                Id = entity.Brand.Id,
            },
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
            Currency = entity.Currency,
        };
    }
    
    public static ClothingDto ToClothingDto(ClothingEntity entity)
    {
        return new ClothingDto()
        {
            Id = entity.Id,
            Article = entity.Article,
            Brand = new()
            {
                Name = entity.Brand.Name,
                Country = entity.Brand.Country,
                Id = entity.Brand.Id,
            },
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
            Currency = entity.Currency,
            Audience = entity.Audience,
            Color = entity.Color,
            Size = entity.Size,
        };
    }
    
    public static ShoesDto ToShoesDto(ShoesEntity entity)
    {
        return new ShoesDto()
        {
            Id = entity.Id,
            Article = entity.Article,
            Brand = new()
            {
                Name = entity.Brand.Name,
                Country = entity.Brand.Country,
                Id = entity.Brand.Id,
            },
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
            Audience = entity.Audience,
            Color = entity.Color,
            Size = entity.Size,
        };
    }

    #endregion
}