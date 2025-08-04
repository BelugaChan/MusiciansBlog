using Microsoft.EntityFrameworkCore;
using MusiciansBlog.API.Converters;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Infrastructure
{
    public sealed class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<BlogEntity> Blogs { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentEntity>()
                .HasOne(c => c.ParentBlog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.ParentBlogId);

            modelBuilder.Entity<CommentEntity>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<BlogEntity>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<CommentEntity>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<BlogEntity>().HasQueryFilter(c => !c.IsDeleted);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetConverter>();
        }
    }
}
