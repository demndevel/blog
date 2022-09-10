using Blog.Models;
using Blog.Repository.Interfaces;

namespace Blog.Repository.Implementations;

public class ProjectRepository : IRepository<Project>
{
    private readonly ApplicationContext _db;
    
    public ProjectRepository(ApplicationContext db)
    {
        _db = db;
    }
    
    public Project GetById(long id)
    {
        return _db.Projects.FirstOrDefault(p => p.Id == id)!;
    }

    public long GetCount()
    {
        return _db.Projects.Count();
    }

    public List<Project> GetPagedList(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public void Insert(Project project)
    {
        _db.Projects.Add(project);
    }

    public void Update(long id, Project newProject)
    {
        var oldProject = _db.Projects.FirstOrDefault(p => p.Id == id)!;

        oldProject.Title = newProject.Title;
        oldProject.ShortDescription = newProject.ShortDescription;
        oldProject.Link = newProject.Link;
    }

    public void Delete(Project project)
    {
        _db.Projects.Remove(project);
    }

    public Project[] GetArray()
    {
        return _db.Projects.ToArray();
    }

    public void Save()
    {
        _db.SaveChanges();
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if(disposing)
                _db.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}