using Domain.Entities.Comment;
using Domain.Entities.Note;
using Domain.Entities.Project;
using Domain.Entities.Tag;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Persistence;

public interface IApplicationContext
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}