﻿using eShop.Domain.Entities.ReviewApi;

namespace eShop.ReviewsApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CommentEntity> Comments => Set<CommentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentEntity>(x =>
        {
            x.HasKey(p => p.CommentId);
        });
    }
}