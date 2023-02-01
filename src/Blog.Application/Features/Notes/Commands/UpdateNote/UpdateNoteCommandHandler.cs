using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Note;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : ICommandHandler<UpdateNoteCommand, Unit>
{
    private readonly IApplicationContext _db;

    public UpdateNoteCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(UpdateNoteCommand command, CancellationToken ct)
    {
        var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == command.Id, ct);

        if (note is null)
            throw new NotFoundException(nameof(Note), command.Id.ToString());

        note.Title = new Title(command.Title).Value;
        note.Text = new Text(command.Text).Value;
        note.ShortDescription = new ShortDescription(command.ShortDescription).Value;
        note.Tags = new Domain.Entities.Note.Tags(command.Tags).Value;

        await _db.SaveChangesAsync(ct);
        
        return Unit.Value;
    }
}