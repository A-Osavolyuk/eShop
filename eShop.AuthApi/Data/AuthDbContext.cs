﻿using eShop.Domain.Entities.Admin;

namespace eShop.AuthApi.Data
{
    internal sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<PersonalDataEntity> PersonalData => Set<PersonalDataEntity>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserPermissions> UserPermissions => Set<UserPermissions>();
        public DbSet<UserAuthenticationToken> UserAuthenticationTokens => Set<UserAuthenticationToken>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", Id = "e6d15d97-b803-435a-9dc2-a7c45c08a1af" },
                new IdentityRole() { Name = "User", NormalizedName = "USER", Id = "270910a1-d582-4ce0-8b23-c8141d720064" },
                new IdentityRole() { Name = "Seller", NormalizedName = "SELLER", Id = "26bb7907-e254-41d4-96f0-8afb7deccae4" }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "e6d15d97-b803-435a-9dc2-a7c45c08a1af", UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" },
                new IdentityUserRole<string>() { RoleId = "270910a1-d582-4ce0-8b23-c8141d720064", UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" },
                new IdentityUserRole<string>() { RoleId = "26bb7907-e254-41d4-96f0-8afb7deccae4", UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });

            builder.Entity<PersonalDataEntity>(x =>
            {
                x.HasKey(p => p.Id);
                x.HasOne(x => x.User).WithOne(x => x.PersonalData).HasForeignKey<PersonalDataEntity>(x => x.UserId);

                x.HasData(new PersonalDataEntity()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alexander",
                    LastName = "Osavolyuk",
                    Gender = "Male",
                    DateOfBirth = new DateTime(2004, 2, 11),
                    UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3"
                });
            });

            builder.Entity<Permission>(x =>
            {
                x.HasKey(p => p.Id);

                x.HasData(
                new Permission() { Id = Guid.Parse("dba6e723-ac0f-42a3-91fd-e40bdb08e26b"), Name = "Permission.Account.ManageAccount" },

                new Permission() { Id = Guid.Parse("349898ee-1f26-4877-86ca-0960361b5e3e"), Name = "Permission.Admin.ManageUsers" },
                new Permission() { Id = Guid.Parse("74e0644b-6f9d-4964-a9a6-341a7834cc0e"), Name = "Permission.Admin.ManageLockout" },
                new Permission() { Id = Guid.Parse("e14d7bcf-0ab4-4168-b2b5-ff0894782097"), Name = "Permission.Admin.ManageRoles" },
                new Permission() { Id = Guid.Parse("df258394-6290-43b8-abc9-d52aba8ff6e6"), Name = "Permission.Admin.ManagePermissions" },

                new Permission() { Id = Guid.Parse("3c38ecbf-a14c-4d46-9eab-6b297cca124d"), Name = "Permission.Product.View" },
                new Permission() { Id = Guid.Parse("5034df8e-c656-4f85-b197-7afff97ecad0"), Name = "Permission.Product.Edit" },
                new Permission() { Id = Guid.Parse("25af1455-d0b8-4be3-b6ff-9cf393d59258"), Name = "Permission.Product.Delete" },
                new Permission() { Id = Guid.Parse("a1216fa3-66dd-4a6d-8616-48a7b9900649"), Name = "Permission.Product.Create" });
            });

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

            builder.Entity<UserPermissions>(x =>
            {
                x.HasKey(ur => new { ur.UserId, ur.PermissionId });

                x.HasOne(ur => ur.User)
                .WithMany(u => u.Permissions)
                .HasForeignKey(ur => ur.UserId);

                x.HasOne(ur => ur.Permission)
                .WithMany(r => r.Permissions)
                .HasForeignKey(ur => ur.PermissionId);

                x.HasData(
                    new UserPermissions() { UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3", PermissionId = Guid.Parse("349898ee-1f26-4877-86ca-0960361b5e3e") },
                    new UserPermissions() { UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3", PermissionId = Guid.Parse("74e0644b-6f9d-4964-a9a6-341a7834cc0e") },
                    new UserPermissions() { UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3", PermissionId = Guid.Parse("e14d7bcf-0ab4-4168-b2b5-ff0894782097") },
                    new UserPermissions() { UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3", PermissionId = Guid.Parse("df258394-6290-43b8-abc9-d52aba8ff6e6") },
                    new UserPermissions() { UserId = "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3", PermissionId = Guid.Parse("dba6e723-ac0f-42a3-91fd-e40bdb08e26b") });
            });

            builder.Entity<UserAuthenticationToken>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(x => x.Token).HasColumnType("VARCHAR(MAX)");
                x.HasOne(x => x.User).WithOne(x => x.AuthenticationToken).HasForeignKey<UserAuthenticationToken>(x => x.UserId);
            });
        }
    }
}
