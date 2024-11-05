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
            Name = request.Name,
            Price = new Price()
            {
                Amount = request.Price,
                Currency = request.Currency
            },
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = request.Brand,
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size,
        };
    }
    
    public static ShoesEntity ToShoesEntity(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ShoesEntity()
        {
            Name = request.Name,
            Price = new Price()
            {
                Amount = request.Price,
                Currency = request.Currency
            },
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = request.Brand,
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size,
        };
    }
    
    public static ProductEntity ToProductEntity(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        return new ProductEntity()
        {
            Name = request.Name,
            Price = new Price()
            {
                Amount = request.Price,
                Currency = request.Currency
            },
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = request.Brand,
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
            Price = new Price()
            {
                Amount = request.Price,
                Currency = request.Currency
            },
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = request.Brand,
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
            Price = new Price()
            {
                Amount = request.Price,
                Currency = request.Currency
            },
            Audience = request.Audience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = request.Brand,
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
            Price = new Price()
            {
                Amount = request.Price,
                Currency = request.Currency
            },
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = request.Brand,
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
            Brand = entity.Brand,
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
        };
    }
    
    public static ClothingDto ToClothingDto(ClothingEntity entity)
    {
        return new ClothingDto()
        {
            Id = entity.Id,
            Article = entity.Article,
            Brand = entity.Brand,
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
    
    public static ShoesDto ToShoesDto(ShoesEntity entity)
    {
        return new ShoesDto()
        {
            Id = entity.Id,
            Article = entity.Article,
            Brand = entity.Brand,
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