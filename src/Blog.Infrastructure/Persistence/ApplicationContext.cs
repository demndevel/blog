using Application.Interfaces.Persistence;
using Domain.Entities.Comment;
using Domain.Entities.Note;
using Domain.Entities.Project;
using Domain.Entities.Tag;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated(); // TODO: Remove this line and replace with migrations
    }

    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
}