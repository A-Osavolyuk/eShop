using eShop.Domain.Entities;

namespace eShop.ReviewsApi.Data
{
    public class ReviewDbContext(DbContextOptions<ReviewDbContext> options) : DbContext(options)
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
}
