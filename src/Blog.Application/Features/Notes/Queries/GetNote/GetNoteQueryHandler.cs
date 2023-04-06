using Application.Errors;
using Application.Interfaces;
using Application.Persistence;
using Domain.Entities.Note;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Queries.GetNote;

public class GetNoteQueryHandler : IQueryHandler<GetNoteQuery, GetNoteQueryResult>
{
    private readonly ApplicationContext _db;

    public GetNoteQueryHandler(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetNoteQueryResult> Handle(GetNoteQuery query, CancellationToken ct)
    {
        var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == query.Id, ct);

        if (note == null)
            throw new NotFoundException(nameof(Note), query.Id.ToString());

        var result = new GetNoteQueryResult
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            ShortDescription = note.ShortDescription,
            Date = note.Date,
            Tags = note.Tags,
            Views = note.Views
        };
        
        return result;
    }
}