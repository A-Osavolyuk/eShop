using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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
                    TwoFactorEnabled = false,

                    FirstName = "Alex",
                    LastName = "Osavoliuk",
                    DateOfBirth = new DateTime(2000, 01, 01),
                    Gender = "Male",
                    PhoneNumber = "380686100242"
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", Id = "e6d15d97-b803-435a-9dc2-a7c45c08a1af" },
                new IdentityRole() { Name = "User", NormalizedName = "USER", Id = "270910a1-d582-4ce0-8b23-c8141d720064" },
                new IdentityRole() { Name = "Seller", NormalizedName = "SELLER", Id = "26bb7907-e254-41d4-96f0-8afb7deccae4" }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "e6d15d97-b803-435a-9dc2-a7c45c08a1af", UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });
        }
    }
}

