using Application.Interfaces;
using Application.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Queries.GetAllNotes;

public class GetAllNotesQueryHandler : IQueryHandler<GetAllNotesQuery, GetAllNotesQueryResult>
{
    private readonly ApplicationContext _db;

    public GetAllNotesQueryHandler(ApplicationContext db)
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