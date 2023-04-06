using Domain.Entities.Comment;
using Domain.Entities.Note;
using Domain.Entities.Project;
using Domain.Entities.Tag;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }

    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
}