using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Queries.GetNotesByPage;

public class GetNotesByPageQueryHandler : IQueryHandler<GetNotesByPageQuery, GetNotesByPageQueryResult>
{
    private readonly IApplicationContext _db;

    public GetNotesByPageQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetNotesByPageQueryResult> Handle(GetNotesByPageQuery query, CancellationToken ct)
    {
        var notes = await _db.Notes
            .OrderByDescending(n => n.Date)
            .Skip(query.Page * 10)
            .Take(10)
            .ProjectToType<GetNotesByPageQueryResultItem>()
            .ToListAsync(ct);
        
        return new GetNotesByPageQueryResult { Notes = notes };
    }
}