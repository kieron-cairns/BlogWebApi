using BlogWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Interfaces
{
    public interface IBlogPostContext
    {
        DbSet<BlogPost> Posts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
