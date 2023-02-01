using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Project;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand, Unit>
{
    private readonly IApplicationContext _db;

    public DeleteProjectCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken ct)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, ct);

        if (project is null) throw new NotFoundException(nameof(Project), request.Id.ToString());

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync(ct);
        
        return Unit.Value;
    }
}