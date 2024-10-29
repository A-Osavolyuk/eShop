using eShop.ProductWebApi.Extensions;

namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<ClothingEntity> Clothing => Set<ClothingEntity>();
        public DbSet<BrandEntity> Brands => Set<BrandEntity>();
        public DbSet<ShoesEntity> Shoes => Set<ShoesEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrandEntity>(x =>
            {
                x.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ProductEntity>(x =>
            {
                x.UseTptMappingStrategy();
                x.HasKey(x => x.Id);
                x.HasOne(x => x.BrandEntity).WithMany().HasForeignKey(x => x.BrandId);
            });


            modelBuilder.GenerateSeedData();
        }
    }
}
