using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Note;

namespace Application.Features.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand, long>
{
    private readonly IApplicationContext _db;

    public CreateNoteCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<long> Handle(CreateNoteCommand command, CancellationToken ct)
    {
        var note = GetValidatedNote(command);

        await _db.Notes.AddAsync(note, ct);
        await _db.SaveChangesAsync(ct);

        return note.Id;
    }

    private Note GetValidatedNote(CreateNoteCommand command)
    {
        return new Note
        {
            Text = new Text(command.Text).Value,
            Tags = command.Tags,
            Date = DateTime.Now,
            Title = new Title(command.Title).Value,
            ShortDescription = new ShortDescription(command.ShortDescription).Value
        };
    }
}