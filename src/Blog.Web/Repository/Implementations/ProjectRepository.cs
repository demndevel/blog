using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Web.Repository.Interfaces;

namespace Web.Repository.Implementations;

public class ProjectRepository : IRepository<Project>
{
    private readonly ApplicationContext _db;
    
    public ProjectRepository(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<Project> GetById(long id)
    {
        return (await _db.Projects.FirstOrDefaultAsync(p => p.Id == id))!;
    }

    public async Task<long> GetCount()
    {
        return await _db.Projects.CountAsync();
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

    public async Task<Project[]> GetArray()
    {
        return await _db.Projects.ToArrayAsync();
    }
}