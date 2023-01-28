using Application.Interfaces.Persistence;
using Domain.Entities;
using Domain.Entities.Tag;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
}