namespace eShop.ProductWebApi.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Clothing> Clothing => Set<Clothing>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Shoes> Shoes => Set<Shoes>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().UseTptMappingStrategy();

            modelBuilder.Entity<Supplier>().HasKey(x => x.Id);
            modelBuilder.Entity<Brand>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<Product>().HasOne(x => x.Supplier).WithMany().HasForeignKey(x => x.SupplierId);
            modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany().HasForeignKey(x => x.BrandId);

            modelBuilder.Entity<Product>().OwnsOne(x => x.Price, p =>
            {
                p.Property(p => p.Currency).HasColumnName("Currency");
                p.Property(p => p.Amount).HasColumnName("Amount");
            });

            modelBuilder.Entity<Brand>().HasData(
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

            modelBuilder.Entity<Supplier>().HasData(
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
                },
                new Supplier()
                {
                    Id = Guid.Parse("90a3052c-8967-4d97-ae80-779af12c44c1"),
                    Name = "Puma Inc.",
                    ContactEmail = "puma@gmail.com",
                    ContactPhone = "123456789003"
                },
                new Supplier()
                {
                    Id = Guid.Parse("450c2a3e-eb97-4d5e-b1d7-891649c57b92"),
                    Name = "Reebok Inc.",
                    ContactEmail = "reebok@gmail.com",
                    ContactPhone = "123456789004"
                },
                new Supplier()
                {
                    Id = Guid.Parse("4f13e7bb-4b4c-4e12-a0fc-58cb09c4cf17"),
                    Name = "Under Armour Inc.",
                    ContactEmail = "underarmour@gmail.com",
                    ContactPhone = "123456789005"
                },
                new Supplier()
                {
                    Id = Guid.Parse("f8c8b35b-26a5-4029-a1a1-53a9d40b3697"),
                    Name = "New Balance Inc.",
                    ContactEmail = "newbalance@gmail.com",
                    ContactPhone = "123456789006"
                },
                new Supplier()
                {
                    Id = Guid.Parse("f48a6e4b-f73f-42a9-a23e-4be702f671b4"),
                    Name = "Converse Inc.",
                    ContactEmail = "converse@gmail.com",
                    ContactPhone = "123456789007"
                },
                new Supplier()
                {
                    Id = Guid.Parse("80cc7840-f39c-49d0-80b5-4d8d9b9c0c36"),
                    Name = "Vans Inc.",
                    ContactEmail = "vans@gmail.com",
                    ContactPhone = "123456789008"
                },
                new Supplier()
                {
                    Id = Guid.Parse("686a4e3f-2c29-47c7-8f2c-cb1688c962f5"),
                    Name = "ASICS Inc.",
                    ContactEmail = "asics@gmail.com",
                    ContactPhone = "123456789009"
                });

        }
    }
}
