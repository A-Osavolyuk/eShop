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
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(x => x.Subcategory)
                .WithMany()
                .HasForeignKey(x => x.SubcategoryId);

            modelBuilder.Entity<CategoryEntity>()
                .HasMany(x => x.Subcategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity { CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), Name = "Electronics" },
                new CategoryEntity { CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), Name = "Clothing" },
                new CategoryEntity { CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), Name = "Books" },
                new CategoryEntity { CategoryId = Guid.Parse("b390fb0f-b2af-470d-bab4-d719782dce69"), Name = "Appliances" },
                new CategoryEntity { CategoryId = Guid.Parse("7cb94851-0e4f-4044-b4a7-16e4af51a605"), Name = "Sports and Fitness" },
                new CategoryEntity { CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), Name = "Beauty and Personal Care" },
                new CategoryEntity { CategoryId = Guid.Parse("4ad9941d-8045-47a7-a55a-9093011509ce"), Name = "Home and Kitchen" },
                new CategoryEntity { CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), Name = "Toys and Games" },
                new CategoryEntity { CategoryId = Guid.Parse("8803ed5e-afe6-42d0-8c20-7e1bbf7cc79f"), Name = "Health and Wellness" },
                new CategoryEntity { CategoryId = Guid.Parse("20a7b72c-a789-471c-bbcb-2f86721d0f3c"), Name = "Automotive" },
                new CategoryEntity { CategoryId = Guid.Parse("8e19ab30-aef1-4d96-89b0-67bd70573549"), Name = "Tools and Home Improvement" },
                new CategoryEntity { CategoryId = Guid.Parse("0702af9f-91e8-43b6-b5b5-aaad2fdff7f1"), Name = "Pet Supplies" },
                new CategoryEntity { CategoryId = Guid.Parse("f98cf058-d8ab-4cc5-b1eb-20486517a76e"), Name = "Office Products" },
                new CategoryEntity { CategoryId = Guid.Parse("2b484ef4-c2ff-4e5d-9706-dc4c9e7963cb"), Name = "Grocery and Gourmet Food" },
                new CategoryEntity { CategoryId = Guid.Parse("32421dbb-c408-4e3d-a0f2-ff625c367a82"), Name = "Jewelry and Watches" },
                new CategoryEntity { CategoryId = Guid.Parse("a61cc7e6-0b2a-4710-a272-3153f6511214"), Name = "Musical Instruments" },
                new CategoryEntity { CategoryId = Guid.Parse("5fbe0760-8e80-455d-afe0-c51a99a4b613"), Name = "Industrial and Scientific" },
                new CategoryEntity { CategoryId = Guid.Parse("27951285-5647-402f-b137-e83fcf010a6d"), Name = "Baby Products" },
                new CategoryEntity { CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), Name = "Movies and TV" },
                new CategoryEntity { CategoryId = Guid.Parse("5f4f58ca-f69e-4913-af64-6e7bd91ccd09"), Name = "Handmade" });

            modelBuilder.Entity<SubcategoryEntity>().HasData(
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), Name = "Computers" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), Name = "Smartphones" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d1fd53bd-1279-4537-98f5-8074d420e373"), Name = "TVs and Home Entertainment" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), Name = "Men's Clothing" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), Name = "Women's Clothing" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0caa8271-7310-4587-ad0d-354a5de55438"), Name = "Kids' Clothing" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), Name = "Fiction" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), Name = "Non-Fiction" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("d74b985d-332a-450d-82d4-f10cdc06923e"), Name = "Children's Books" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("b390fb0f-b2af-470d-bab4-d719782dce69"), Name = "Kitchen Appliances" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("b390fb0f-b2af-470d-bab4-d719782dce69"), Name = "Home Appliances" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("7cb94851-0e4f-4044-b4a7-16e4af51a605"), Name = "Fitness Equipment" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("7cb94851-0e4f-4044-b4a7-16e4af51a605"), Name = "Sports Gear" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), Name = "Skincare" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), Name = "Haircare" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f8099a52-5f54-410a-aa66-468bf1a4f909"), Name = "Makeup" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("4ad9941d-8045-47a7-a55a-9093011509ce"), Name = "Furniture" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("4ad9941d-8045-47a7-a55a-9093011509ce"), Name = "Kitchenware" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), Name = "Action Figures" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), Name = "Board Games" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0d5074f6-d20a-486d-8614-5be2f06772fd"), Name = "Puzzles" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8803ed5e-afe6-42d0-8c20-7e1bbf7cc79f"), Name = "Vitamins and Supplements" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8803ed5e-afe6-42d0-8c20-7e1bbf7cc79f"), Name = "Fitness Trackers" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("20a7b72c-a789-471c-bbcb-2f86721d0f3c"), Name = "Car Parts" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("20a7b72c-a789-471c-bbcb-2f86721d0f3c"), Name = "Car Accessories" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8e19ab30-aef1-4d96-89b0-67bd70573549"), Name = "Power Tools" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("8e19ab30-aef1-4d96-89b0-67bd70573549"), Name = "Home Renovation" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0702af9f-91e8-43b6-b5b5-aaad2fdff7f1"), Name = "Dog Supplies" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("0702af9f-91e8-43b6-b5b5-aaad2fdff7f1"), Name = "Cat Supplies" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f98cf058-d8ab-4cc5-b1eb-20486517a76e"), Name = "Stationery" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("f98cf058-d8ab-4cc5-b1eb-20486517a76e"), Name = "Office Furniture" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("2b484ef4-c2ff-4e5d-9706-dc4c9e7963cb"), Name = "Snacks" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("2b484ef4-c2ff-4e5d-9706-dc4c9e7963cb"), Name = "Beverages" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("32421dbb-c408-4e3d-a0f2-ff625c367a82"), Name = "Watches" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("32421dbb-c408-4e3d-a0f2-ff625c367a82"), Name = "Fine Jewelry" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("a61cc7e6-0b2a-4710-a272-3153f6511214"), Name = "Guitars" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("a61cc7e6-0b2a-4710-a272-3153f6511214"), Name = "Keyboards" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5fbe0760-8e80-455d-afe0-c51a99a4b613"), Name = "Lab Equipment" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5fbe0760-8e80-455d-afe0-c51a99a4b613"), Name = "Industrial Tools" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("27951285-5647-402f-b137-e83fcf010a6d"), Name = "Diapers and Wipes" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("27951285-5647-402f-b137-e83fcf010a6d"), Name = "Baby Gear" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), Name = "Action" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), Name = "Comedy" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("6de0ac2a-1f47-4af5-8095-3470f1f80dfb"), Name = "Drama" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5f4f58ca-f69e-4913-af64-6e7bd91ccd09"), Name = "Handmade Jewelry" },
                new SubcategoryEntity { SubcategoryId = Guid.NewGuid(), CategoryId = Guid.Parse("5f4f58ca-f69e-4913-af64-6e7bd91ccd09"), Name = "Handmade Home Decor" });
        }
    }
}
