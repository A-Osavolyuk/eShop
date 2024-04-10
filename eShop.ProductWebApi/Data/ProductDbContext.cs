using eShop.Domain.Entities;
using eShop.Domain.Enums;

namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Clothing> Clothing => Set<Clothing>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Shoes> Shoes => Set<Shoes>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().UseTptMappingStrategy();

            modelBuilder.Entity<Supplier>().HasKey(x => x.Id);
            modelBuilder.Entity<Brand>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<Product>().HasOne(x => x.Supplier).WithMany(x => x.Products).HasForeignKey(x => x.SupplierId);
            modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandId);

            modelBuilder.Entity<Product>().OwnsOne(x => x.Price, p =>
            {
                p.Property(p => p.Currency).HasColumnName("Currency");
                p.Property(p => p.Amount).HasColumnName("Amount");
            });
        }
    }
}
