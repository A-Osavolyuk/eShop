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
                x.HasMany(x => x.Goods).WithOne().HasForeignKey(x => x.Id);
                x.HasData(
                    new Cart() { CartId = Guid.Parse("f7c4fb85-3d56-4fbe-82b7-feaedbd92bcf"), UserId = Guid.Parse("abb9d2ed-c3d2-4df9-ba88-eab018b95bc3") });
            });

            modelBuilder.Entity<Good>(x => {
                x.HasKey(x => x.Id);
            });
        }
    }
}
