using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Note;

namespace Application.Features.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : ICommandHandler<DeleteNoteCommand, Unit>
{
    private readonly IApplicationContext _db;

    public DeleteNoteCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(DeleteNoteCommand command, CancellationToken ct)
    {
        var note = _db.Notes.FirstOrDefault(n => n.Id == command.Id);
        
        if (note == null)
            throw new NotFoundException(nameof(Note), command.Id.ToString());

        _db.Notes.Remove(note);
        await _db.SaveChangesAsync(ct);
        
        return Unit.Value;
    }
}