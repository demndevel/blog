using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Queries.GetAllNotes;

public class GetAllNotesQueryHandler : IQueryHandler<GetAllNotesQuery, GetAllNotesQueryResult>
{
    private readonly IApplicationContext _db;

    public GetAllNotesQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetAllNotesQueryResult> Handle(GetAllNotesQuery query, CancellationToken ct)
    {
        var notes = await _db.Notes
            .ProjectToType<GetAllNotesQueryResultItem>()
            .ToListAsync(ct);

        return new GetAllNotesQueryResult { Notes = notes };
    }
}