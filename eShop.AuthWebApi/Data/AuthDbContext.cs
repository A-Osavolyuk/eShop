using eShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                    Email = "test@test.com",
                    NormalizedEmail = "test@test.com".ToUpper(),
                    UserName = "test@test.com",
                    NormalizedUserName = "test@test.com".ToUpper(),
                    PasswordHash = "AQAAAAIAAYagAAAAEHeZ7iJce/rkJIBOAFdarWHCG1NUYQ1y67q5EyVGG9ttMlkXR2wxOMAQRsg+HtNtCg==",
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                });
        }
    }
}
