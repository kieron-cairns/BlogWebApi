using BlogWebApi.Interfaces;
using BlogWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Data
{
    public class BlogPostContext : DbContext, IBlogPostContext
    {
        public DbSet<BlogPost> Posts { get; set; } = null!;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
