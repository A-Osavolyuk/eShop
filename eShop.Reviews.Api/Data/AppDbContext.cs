﻿using eShop.Reviews.Api.Entities;

namespace eShop.Reviews.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CommentEntity> Comments => Set<CommentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentEntity>(x => { x.HasKey(p => p.CommentId); });
    }
}