using eShop.ProductWebApi.Extensions;

namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Clothing> Clothing => Set<Clothing>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Shoes> Shoes => Set<Shoes>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(x =>
            {
                x.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Product>(x =>
            {
                x.UseTptMappingStrategy();
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Brand).WithMany().HasForeignKey(x => x.BrandId);
            });


            modelBuilder.GenerateSeedData();
        }
    }
}
