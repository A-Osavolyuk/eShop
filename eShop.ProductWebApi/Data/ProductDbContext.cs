namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<SupplierEntity> Suppliers => Set<SupplierEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<SubcategoryEntity> Subcategories => Set<SubcategoryEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasKey(x => x.ProductId);
            modelBuilder.Entity<CategoryEntity>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<SupplierEntity>().HasKey(x => x.SupplierId);
            modelBuilder.Entity<SubcategoryEntity>().HasKey(x => x.SubcategoryId);

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
                .HasOne(product => product.Supplier)
                .WithMany()
                .HasForeignKey(product => product.SupplierId);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(product => product.Subcategory)
                .WithMany()
                .HasForeignKey(product => product.SubcategoryId);

            modelBuilder.Entity<CategoryEntity>()
                .HasMany(category => category.Subcategories)
                .WithOne(subcategory => subcategory.Category)
                .HasForeignKey(subcategory => subcategory.CategoryId);

            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity { CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), CategoryName = "Electronics" },
                new CategoryEntity { CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), CategoryName = "Clothing" },
                new CategoryEntity { CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), CategoryName = "Books" },
                new CategoryEntity { CategoryId = Guid.Parse("b390fb0f-b2af-470d-bab4-d719782dce69"), CategoryName = "Appliances" },
                new CategoryEntity { CategoryId = Guid.Parse("7cb94851-0e4f-4044-b4a7-16e4af51a605"), CategoryName = "Sports and Fitness" },
                new CategoryEntity { CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), CategoryName = "Beauty and Personal Care" },
                new CategoryEntity { CategoryId = Guid.Parse("4ad9941d-8045-47a7-a55a-9093011509ce"), CategoryName = "Home and Kitchen" },
                new CategoryEntity { CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), CategoryName = "Toys and Games" },
                new CategoryEntity { CategoryId = Guid.Parse("8803ed5e-afe6-42d0-8c20-7e1bbf7cc79f"), CategoryName = "Health and Wellness" },
                new CategoryEntity { CategoryId = Guid.Parse("20a7b72c-a789-471c-bbcb-2f86721d0f3c"), CategoryName = "Automotive" },
                new CategoryEntity { CategoryId = Guid.Parse("8e19ab30-aef1-4d96-89b0-67bd70573549"), CategoryName = "Tools and Home Improvement" },
                new CategoryEntity { CategoryId = Guid.Parse("0702af9f-91e8-43b6-b5b5-aaad2fdff7f1"), CategoryName = "Pet Supplies" },
                new CategoryEntity { CategoryId = Guid.Parse("f98cf058-d8ab-4cc5-b1eb-20486517a76e"), CategoryName = "Office Products" },
                new CategoryEntity { CategoryId = Guid.Parse("2b484ef4-c2ff-4e5d-9706-dc4c9e7963cb"), CategoryName = "Grocery and Gourmet Food" },
                new CategoryEntity { CategoryId = Guid.Parse("32421dbb-c408-4e3d-a0f2-ff625c367a82"), CategoryName = "Jewelry and Watches" },
                new CategoryEntity { CategoryId = Guid.Parse("a61cc7e6-0b2a-4710-a272-3153f6511214"), CategoryName = "Musical Instruments" },
                new CategoryEntity { CategoryId = Guid.Parse("5fbe0760-8e80-455d-afe0-c51a99a4b613"), CategoryName = "Industrial and Scientific" },
                new CategoryEntity { CategoryId = Guid.Parse("27951285-5647-402f-b137-e83fcf010a6d"), CategoryName = "Baby Products" },
                new CategoryEntity { CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), CategoryName = "Movies and TV" },
                new CategoryEntity { CategoryId = Guid.Parse("5f4f58ca-f69e-4913-af64-6e7bd91ccd09"), CategoryName = "Handmade" });

            modelBuilder.Entity<SubcategoryEntity>().HasData(
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), SubcategoryName = "Computers" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), SubcategoryName = "Smartphones" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), SubcategoryName = "TVs and Home Entertainment" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), SubcategoryName = "Men's Clothing" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), SubcategoryName = "Women's Clothing" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), SubcategoryName = "Kids' Clothing" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), SubcategoryName = "Fiction" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), SubcategoryName = "Non-Fiction" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), SubcategoryName = "Children's Books" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("b390fb0f-b2af-470d-bab4-d719782dce69"), SubcategoryName = "Kitchen Appliances" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("b390fb0f-b2af-470d-bab4-d719782dce69"), SubcategoryName = "Home Appliances" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("7cb94851-0e4f-4044-b4a7-16e4af51a605"), SubcategoryName = "Fitness Equipment" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("7cb94851-0e4f-4044-b4a7-16e4af51a605"), SubcategoryName = "Sports Gear" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), SubcategoryName = "Skincare" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), SubcategoryName = "Haircare" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), SubcategoryName = "Makeup" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("4ad9941d-8045-47a7-a55a-9093011509ce"), SubcategoryName = "Furniture" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("4ad9941d-8045-47a7-a55a-9093011509ce"), SubcategoryName = "Kitchenware" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), SubcategoryName = "Action Figures" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), SubcategoryName = "Board Games" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), SubcategoryName = "Puzzles" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8803ed5e-afe6-42d0-8c20-7e1bbf7cc79f"), SubcategoryName = "Vitamins and Supplements" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8803ed5e-afe6-42d0-8c20-7e1bbf7cc79f"), SubcategoryName = "Fitness Trackers" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("20a7b72c-a789-471c-bbcb-2f86721d0f3c"), SubcategoryName = "Car Parts" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("20a7b72c-a789-471c-bbcb-2f86721d0f3c"), SubcategoryName = "Car Accessories" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8e19ab30-aef1-4d96-89b0-67bd70573549"), SubcategoryName = "Power Tools" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8e19ab30-aef1-4d96-89b0-67bd70573549"), SubcategoryName = "Home Renovation" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0702af9f-91e8-43b6-b5b5-aaad2fdff7f1"), SubcategoryName = "Dog Supplies" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0702af9f-91e8-43b6-b5b5-aaad2fdff7f1"), SubcategoryName = "Cat Supplies" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f98cf058-d8ab-4cc5-b1eb-20486517a76e"), SubcategoryName = "Stationery" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f98cf058-d8ab-4cc5-b1eb-20486517a76e"), SubcategoryName = "Office Furniture" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("2b484ef4-c2ff-4e5d-9706-dc4c9e7963cb"), SubcategoryName = "Snacks" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("2b484ef4-c2ff-4e5d-9706-dc4c9e7963cb"), SubcategoryName = "Beverages" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("32421dbb-c408-4e3d-a0f2-ff625c367a82"), SubcategoryName = "Watches" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("32421dbb-c408-4e3d-a0f2-ff625c367a82"), SubcategoryName = "Fine Jewelry" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("a61cc7e6-0b2a-4710-a272-3153f6511214"), SubcategoryName = "Guitars" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("a61cc7e6-0b2a-4710-a272-3153f6511214"), SubcategoryName = "Keyboards" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5fbe0760-8e80-455d-afe0-c51a99a4b613"), SubcategoryName = "Lab Equipment" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5fbe0760-8e80-455d-afe0-c51a99a4b613"), SubcategoryName = "Industrial Tools" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("27951285-5647-402f-b137-e83fcf010a6d"), SubcategoryName = "Diapers and Wipes" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("27951285-5647-402f-b137-e83fcf010a6d"), SubcategoryName = "Baby Gear" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), SubcategoryName = "Action" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), SubcategoryName = "Comedy" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), SubcategoryName = "Drama" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5f4f58ca-f69e-4913-af64-6e7bd91ccd09"), SubcategoryName = "Handmade Jewelry" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5f4f58ca-f69e-4913-af64-6e7bd91ccd09"), SubcategoryName = "Handmade Home Decor" });

            modelBuilder.Entity<SupplierEntity>().HasData(
                new SupplierEntity() { SupplierId = Guid.NewGuid(), SupplierName = "Motorola Inc.", ContactEmail = "motorola@gmail.com" });
        }
    }
}
