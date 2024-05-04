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

            builder.Entity<Supplier>().HasData(
                new Supplier()
                {
                    Id = Guid.Parse("7374a58c-59e8-4c86-9c19-38574d664a43"),
                    Name = "Nike Inc.",
                    ContactEmail = "nike@gmail.com",
                    ContactPhone = "123456789001"
                },
                new Supplier()
                {
                    Id = Guid.Parse("fa663b63-cc14-4f0a-bad1-29e4b326eb99"),
                    Name = "Adidas Inc.",
                    ContactEmail = "adidas@gmail.com",
                    ContactPhone = "123456789002"
                });

            builder.Entity<Clothing>(x =>
            {
                x.HasData(
                    new Clothing()
                    {
                        Id = Guid.Parse("39754918-11f8-4e73-8c0e-116f3eef899a"),
                        Article = 112233445566,
                        Audience = Audience.Unisex,
                        BrandId = Guid.Parse("e94bfb2b-33eb-48dc-b0e3-a0a605ea16c7"),
                        SupplierId = Guid.Parse("7374a58c-59e8-4c86-9c19-38574d664a43"),
                        Colors = [ProductColor.Black, ProductColor.White, ProductColor.Red, ProductColor.Blue],
                        Sizes = [ProductSize.XXS, ProductSize.XS, ProductSize.S, ProductSize.M, ProductSize.L, ProductSize.XL],
                        Compound = "100% cotton",
                        Description = "Nike Air Max sneakers are a legendary line within the Nike " +
                        "brand, recognized worldwide for their groundbreaking design and revolutionary technology. " +
                        "Combining style with functionality, these sneakers have become cultural " +
                        "icons, loved by athletes, sneakerheads, and fashion enthusiasts alike.\r\n\r\n" +
                        "At the heart of the Air Max line is the visible Air cushioning unit, which debuted in 1987 with the release of the Air Max 1. " +
                        "This innovative feature not only provided exceptional comfort and support but also became a defining aesthetic element of the shoes. " +
                        "Over the years, Nike has continued to evolve the Air Max technology, offering variations in cushioning systems " +
                        "to suit different preferences and performance needs.\r\n\r\n" +
                        "The Air Max line encompasses a diverse range of models, each with its own unique design elements and features. " +
                        "From classic silhouettes like the Air Max 90 and Air Max 95 to more modern interpretations such as the Air " +
                        "Max 270 and Air Max VaporMax, there's a style for every taste and occasion.\r\n\r\n" +
                        "With their bold colorways, sleek profiles, and iconic Swoosh branding, Nike Air Max sneakers make a statement both on and off the streets. " +
                        "Whether you're hitting the gym, running errands, or simply hanging out with friends, " +
                        "Air Max shoes provide the perfect blend of comfort, style, and performance.\r\n\r\n" +
                        "In summary, Nike Air Max sneakers are more than just shoes—they're symbols of innovation, self-expression, and cultural significance, " +
                        "embodying the spirit of Nike's commitment to pushing boundaries and redefining what's possible in footwear design.",
                        Name = "Nike Air Max",
                        ProductType = ProductType.Clothing
                    });
                x.OwnsOne(x => x.Price).HasData(
                    new
                    {
                        ProductId = Guid.Parse("39754918-11f8-4e73-8c0e-116f3eef899a"),
                        Amount = 300m,
                        Currency = Currency.Euro,
                    });
            });
        }
    }
}
