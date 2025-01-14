namespace eShop.Product.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<SellerEntity> Sellers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    //public DbSet<SellerProductsEntity> SellerProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(x =>
        {
            x.UseTptMappingStrategy();
            x.HasKey(entity => entity.Id);
            x.HasOne(entity => entity.Seller).WithOne().HasForeignKey<ProductEntity>(entity => entity.SellerId);
            x.HasOne(entity => entity.Brand).WithOne().HasForeignKey<ProductEntity>(entity => entity.BrandId);
        });
        
        modelBuilder.Entity<ClothingEntity>().ToTable("Clothing");
        modelBuilder.Entity<ShoesEntity>().ToTable("Shoes");
        
        modelBuilder.Entity<BrandEntity>(x =>
        {
            x.HasKey(entity => entity.Id);
        });

        modelBuilder.Entity<SellerEntity>(x =>
        {
            x.HasKey(entity => entity.Id);
        });

        modelBuilder.Entity<SellerProductsEntity>(x =>
        {
            x.HasKey(entity => new { entity.SellerId, entity.ProductId });
            // x.HasOne(entity => entity.Seller).WithOne().HasForeignKey<SellerProductsEntity>(entity => entity.SellerId);
            // x.HasOne(entity => entity.Product).WithOne().HasForeignKey<SellerProductsEntity>(entity => entity.ProductId);
        });
    }
}