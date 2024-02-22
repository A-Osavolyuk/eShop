using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<ProductCategoryEntity> Categories => Set<ProductCategoryEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductCategoryEntity>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<Supplier>().HasKey(x => x.SupplierId);

            modelBuilder.Entity<ProductEntity>()
                .OwnsOne(x => x.ProductDescription, sa =>
                {
                    sa.Property(p => p.ShortDescription)
                    .HasColumnName("ShortDescription")
                    .HasMaxLength(128);

                    sa.Property(p => p.LongDescription)
                    .HasColumnName("LongDescription")
                    .HasMaxLength(512);
                });
                
            modelBuilder.Entity<ProductEntity>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
