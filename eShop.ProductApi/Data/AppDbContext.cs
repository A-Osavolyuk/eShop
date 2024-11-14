namespace eShop.ProductApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<SellerEntity> Sellers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<SellerProductsEntity> SellerProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(x =>
        {
            x.HasKey(x => x.Id);
            x.UseTptMappingStrategy();
        });
        
        modelBuilder.Entity<ClothingEntity>().ToTable("Clothing");
        modelBuilder.Entity<ShoesEntity>().ToTable("Shoes");
        
        modelBuilder.Entity<BrandEntity>(x =>
        {
            x.HasKey(x => x.Id);
        });

        modelBuilder.Entity<SellerEntity>(x =>
        {
            x.HasKey(x => x.Id);
        });

        modelBuilder.Entity<SellerProductsEntity>(x =>
        {
            x.HasKey(x => new { x.SellerId, x.ProductId });
            x.HasOne(x => x.Seller).WithOne().HasForeignKey<SellerProductsEntity>(x => x.SellerId);
            x.HasOne(x => x.Product).WithOne().HasForeignKey<SellerProductsEntity>(x => x.ProductId);
        });
    }
}