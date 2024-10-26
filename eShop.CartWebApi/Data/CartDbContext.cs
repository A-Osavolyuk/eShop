using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.CartWebApi.Data
{
    public class CartDbContext(DbContextOptions<CartDbContext> options) : DbContext(options)
    {
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(x =>
            {
                x.HasKey(x => x.CartId);
                x.HasMany(x => x.CartItems).WithOne().HasForeignKey(x => x.Id);
            });

            modelBuilder.Entity<CartItem>(x => {
                x.HasKey(x => x.Id);
            });
        }
    }
}
