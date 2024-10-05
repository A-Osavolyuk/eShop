using eShop.Domain.Entities.Admin;

namespace eShop.AuthWebApi.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<PersonalData> PersonalData => Set<PersonalData>();
        public DbSet<Permission> Permissions => Set<Permission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PersonalData>(x =>
            {
                x.HasKey(p => p.Id);
                x.HasOne(x => x.User).WithOne(x => x.PersonalData).HasForeignKey<PersonalData>(x => x.UserId);
            });

            builder.Entity<Permission>(x =>
            {
                x.HasKey(p =>p.Id);
            });

            builder.Entity<Permission>().HasData(
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Account.ManageAccount" },

                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Admin.ManageUsers" },
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Admin.ManageLockout" },
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Admin.ManageRoles" },
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Admin.ManagePermissions" },

                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Product.View" },
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Product.Edit" },
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Product.Delete" },
                new Permission() { Id = Guid.NewGuid(), Name = "Permission.Product.Create" });

            builder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Email = "sasha.osavolll111@gmail.com",
                    NormalizedEmail = "sasha.osavolll111@gmail.com".ToUpper(),
                    UserName = "sasha.osavolll111@gmail.com",
                    NormalizedUserName = "sasha.osavolll111@gmail.com".ToUpper(),
                    PasswordHash = "AQAAAAIAAYagAAAAEHeZ7iJce/rkJIBOAFdarWHCG1NUYQ1y67q5EyVGG9ttMlkXR2wxOMAQRsg+HtNtCg==",
                    Id = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    PhoneNumber = "380686100242",
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", Id = "e6d15d97-b803-435a-9dc2-a7c45c08a1af" },
                new IdentityRole() { Name = "User", NormalizedName = "USER", Id = "270910a1-d582-4ce0-8b23-c8141d720064" },
                new IdentityRole() { Name = "Seller", NormalizedName = "SELLER", Id = "26bb7907-e254-41d4-96f0-8afb7deccae4" }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "e6d15d97-b803-435a-9dc2-a7c45c08a1af", UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });

            builder.Entity<PersonalData>().HasData(new PersonalData()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alexander",
                LastName = "Osavolyuk",
                Gender = "Male",
                DateOfBirth = new DateTime(2004, 2, 11),
                UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3"
            });
        }
    }
}

