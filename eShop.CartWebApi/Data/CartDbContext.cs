using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.CartWebApi.Data
{
    public class CartDbContext(DbContextOptions<CartDbContext> options) : DbContext(options)
    {
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Good> Goods => Set<Good>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(x =>
            {
                x.HasKey(x => x.CartId);
                x.HasMany(x => x.Goods).WithOne().HasForeignKey(x => x.GoodId);
            });

            modelBuilder.Entity<Good>(x => {
                x.HasKey(x => x.GoodId);
            });
        }
    }
}
