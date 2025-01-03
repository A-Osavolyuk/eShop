using eShop.Domain.DTOs.ProductApi;
using eShop.Domain.Entities.ProductApi;
using eShop.Domain.Requests.ProductApi.Product;

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
            ProductCurrency = request.ProductCurrency,
            ProductAudience = request.ProductAudience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = BrandMapper.ToBrandEntity(request.Brand),
            Seller = SellerMapper.ToSellerDto(request.Seller),
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size.ToList(),
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
            ProductCurrency = request.ProductCurrency,
            ProductAudience = request.ProductAudience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = BrandMapper.ToBrandEntity(request.Brand),
            Seller = SellerMapper.ToSellerDto(request.Seller),
            Color = request.Color,
            Description = request.Description,
            Images = request.Images,
            Size = request.Size.ToList(),
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
            ProductCurrency = request.ProductCurrency,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = BrandMapper.ToBrandEntity(request.Brand), 
            Seller = SellerMapper.ToSellerDto(request.Seller),
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
            ProductCurrency = request.ProductCurrency,
            ProductAudience = request.ProductAudience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = BrandMapper.ToBrandEntity(request.Brand),
            Seller = SellerMapper.ToSellerDto(request.Seller),
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
            ProductCurrency = request.ProductCurrency,
            ProductAudience = request.ProductAudience,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = BrandMapper.ToBrandEntity(request.Brand),
            Seller = SellerMapper.ToSellerDto(request.Seller),
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
            ProductCurrency = request.ProductCurrency,
            ProductType = request.ProductType,
            Article = request.Article,
            Brand = BrandMapper.ToBrandEntity(request.Brand),
            Seller = SellerMapper.ToSellerDto(request.Seller),
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
            Brand = BrandMapper.ToBrandDto(entity.Brand),
            Seller = SellerMapper.ToSellerDto(entity.Seller),
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
            ProductCurrency = entity.ProductCurrency,
        };
    }
    
    public static ClothingDto ToClothingDto(ClothingEntity entity)
    {
        return new ClothingDto()
        {
            Id = entity.Id,
            Article = entity.Article,
            Brand = BrandMapper.ToBrandDto(entity.Brand),
            Seller = SellerMapper.ToSellerDto(entity.Seller),
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
            ProductCurrency = entity.ProductCurrency,
            ProductAudience = entity.ProductAudience,
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
            Brand = BrandMapper.ToBrandDto(entity.Brand),
            Seller = SellerMapper.ToSellerDto(entity.Seller),
            Description = entity.Description,
            Images = entity.Images,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Price = entity.Price,
            ProductAudience = entity.ProductAudience,
            Color = entity.Color,
            Size = entity.Size,
        };
    }

    #endregion
}