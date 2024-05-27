using eShop.Domain.Enums;
using System.Drawing;

namespace eShop.ProductWebApi.Extensions
{
    public static class DbContextExtensions
    {
        public static void GenerateSeedData(this ModelBuilder builder)
        {
            builder.Entity<Brand>().HasData(
                new Brand()
                {
                    Id = Guid.Parse("e94bfb2b-33eb-48dc-b0e3-a0a605ea16c7"),
                    Name = "Nike",
                    Country = "USA"
                },
                new Brand()
                {
                    Id = Guid.Parse("447836a4-c0c4-4a55-8bae-ace0b51abaab"),
                    Name = "Adidas",
                    Country = "USA"
                });
        }
    }
}
