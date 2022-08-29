using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}