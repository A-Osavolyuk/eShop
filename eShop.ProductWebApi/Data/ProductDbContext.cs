using Microsoft.EntityFrameworkCore;

namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
    }
}
