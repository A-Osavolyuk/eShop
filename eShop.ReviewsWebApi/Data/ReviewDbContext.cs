using eShop.Domain.Entities;

namespace eShop.ReviewsWebApi.Data
{
    public class ReviewDbContext(DbContextOptions<ReviewDbContext> options) : DbContext(options)
    {
        public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
        public DbSet<CommentEntity> Comments => Set<CommentEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewEntity>(x =>
            {
                x.HasKey(_ => _.ReviewId);
                x.HasMany(x => x.Comments).WithOne(x => x.Review).HasForeignKey(x => x.ReviewId);
            });

            modelBuilder.Entity<CommentEntity>(x =>
            {
                x.HasKey(_ => _.CommentId);
                x.HasOne(_ => _.Review).WithMany(_ => _.Comments).HasForeignKey(x => x.CommentId);
            });
        }
    }
}
