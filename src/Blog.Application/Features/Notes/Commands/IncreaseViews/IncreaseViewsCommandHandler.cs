using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Note;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Commands.IncreaseViews;

public class IncreaseViewsCommandHandler : ICommandHandler<IncreaseViewsCommand, Unit>
{
    private readonly IApplicationContext _db;
    
    public IncreaseViewsCommandHandler(IApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<Unit> Handle(IncreaseViewsCommand command, CancellationToken ct)
    {
        var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == command.Id, ct);
        
        if (note == null)
            throw new NotFoundException(nameof(Note), command.Id.ToString());
        
        note.Views++;
        await _db.SaveChangesAsync(ct);
        
        return Unit.Value;
    }
}