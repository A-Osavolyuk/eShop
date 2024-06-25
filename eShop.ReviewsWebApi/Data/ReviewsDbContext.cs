using eShop.Domain.Entities;

namespace eShop.ReviewsWebApi.Data
{
    public class ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : DbContext(options)
    {
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(x =>
            {
                x.HasKey(_ => _.ReviewId);
                x.HasMany(x => x.Comments).WithOne(x => x.Review).HasForeignKey(x => x.ReviewId);
            });

            modelBuilder.Entity<Comment>(x =>
            {
                x.HasKey(_ => _.CommentId);
                x.HasOne(_ => _.Review).WithMany(_ => _.Comments).HasForeignKey(x => x.CommentId);
            });
        }
    }
}
