using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Queries.GetNoteArchive;

public class GetNoteArchiveQueryHandler : IQueryHandler<GetNoteArchiveQuery, GetNoteArchiveQueryResult>
{
    private readonly IApplicationContext _db;

    public GetNoteArchiveQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetNoteArchiveQueryResult> Handle(GetNoteArchiveQuery query, CancellationToken ct)
    {
        var notes = await _db.Notes
            .ProjectToType<GetNoteArchiveQueryResultItem>()
            .ToListAsync(ct);

        return new GetNoteArchiveQueryResult { Notes = notes };
    }
}