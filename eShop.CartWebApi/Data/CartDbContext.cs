using eShop.Domain.Entities.Cart;
using Product = eShop.Domain.Entities.Cart.Product;
using Cart = eShop.Domain.Entities.Cart.Cart;

namespace eShop.CartWebApi.Data
{
    public class CartDbContext(DbContextOptions<CartDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartProduct> CartProducts => Set<CartProduct>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(x =>
            {
                x.HasKey(product => product.Id);
            });

            modelBuilder.Entity<Cart>(x =>
            {
                x.HasKey(cart => cart.CartId);
            });

            modelBuilder.Entity<CartProduct>(x =>
            {
                x.HasKey(cartProduct => new { cartProduct.CartId, cartProduct.ProductId });
                x.HasOne(ur => ur.Cart)
                    .WithMany()
                    .HasForeignKey(ur => ur.CartId);

                x.HasOne(ur => ur.Product)
                    .WithMany()
                    .HasForeignKey(ur => ur.ProductId);
            });
        }
    }
}
