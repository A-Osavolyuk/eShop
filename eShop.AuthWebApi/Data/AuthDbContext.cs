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
                    TwoFactorEnabled = true,

                    FirstName = "Alex",
                    LastName = "Osavoliuk",
                    DateOfBirth = new DateTime(2000, 01, 01),
                    Gender = "Male",
                    PhoneNumber = "+(38)-068-610-02-42"
                });
        }
    }
}
