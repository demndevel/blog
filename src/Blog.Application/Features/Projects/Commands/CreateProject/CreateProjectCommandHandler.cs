using Application.Interfaces;
using Application.Persistence;
using Domain.Entities.Project;

namespace Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, long>
{
    private readonly ApplicationContext _db;

    public CreateProjectCommandHandler(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<long> Handle(CreateProjectCommand cmd, CancellationToken ct)
    {
        var project = Validate(cmd);
        await _db.Projects.AddAsync(project, ct);
        await _db.SaveChangesAsync(ct);
        
        return project.Id;
    }
    
    private Project Validate(CreateProjectCommand cmd)
    {
        return new Project
        {
            Title = new Title(cmd.Title).Value,
            ShortDescription = new ShortDescription(cmd.ShortDescription).Value,
            Link = new Link(cmd.Link).Value
        };
    }
}