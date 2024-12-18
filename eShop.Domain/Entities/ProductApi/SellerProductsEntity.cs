namespace eShop.Domain.Entities.ProductApi;

public class SellerProductsEntity
{
    public Guid SellerId { get; set; }
    public Guid ProductId { get; set; }

    //public SellerEntity Seller { get; set; } = null!;
    //public ProductEntity Product { get; set; } = null!;
}