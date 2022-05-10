using Microsoft.EntityFrameworkCore;

namespace NewsRESTapi.Models;

/// <summary>
///     Контекст Б.Д. (если Б.Д. не создана создает ее.)
/// </summary>
public sealed class NewsContext : DbContext
{
    public NewsContext(DbContextOptions<NewsContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<News> News { get; set; } = null!;
}