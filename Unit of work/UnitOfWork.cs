using Blog.Models;
using Blog.Repository.Implementations;
using Blog.Repository.Interfaces;

namespace Blog.Unit_of_work;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationContext _context;
    public IRepository<Note> Notes { get; }
    public ITagRepository Tags { get; }
    public IRepository<Project> Projects { get; }

    private readonly IServiceProvider _services;

    public UnitOfWork(ApplicationContext context, IServiceProvider services)
    {
        _services = services;
        
        _context = context;
        Notes = new NoteRepository(_context);
        Tags = new TagRepository(_context);
        Projects = new ProjectRepository(_context);
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }

    private void Dispose(bool disposing)
    {
        _context.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
