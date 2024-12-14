namespace eShop.Domain.Entities.Product;

public class SellerProductsEntity
{
    public Guid SellerId { get; set; }
    public Guid ProductId { get; set; }

    //public SellerEntity Seller { get; set; } = null!;
    //public ProductEntity Product { get; set; } = null!;
}