using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Notes.Queries.GetNotesByTags;

public class GetNotesByTagsQueryHandler : IQueryHandler<GetNotesByTagsQuery, GetNotesByTagsQueryResult>
{
    private readonly IApplicationContext _db;

    public GetNotesByTagsQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetNotesByTagsQueryResult> Handle(GetNotesByTagsQuery query, CancellationToken ct)
    {
        var includedTagList = query.IncludedTags.Split(';');
        var excludedTagList = query.ExcludedTags.Split(';');

        var allNotes = await _db.Notes
            .ProjectToType<GetNotesByTagsQueryResultItem>()
            .ToListAsync(ct);

        var notes = allNotes
            .Where(n => n.Tags.Split(';')
                .Any(t => includedTagList.Contains(t)))
            .Distinct()
            .ToList();

        notes = excludedTagList
            .Aggregate(notes, (current, tag) => current.Where(n => n.Tags.Split(';').All(t => t != tag))
            .ToList());

        return new GetNotesByTagsQueryResult { Notes = notes };
    }
}