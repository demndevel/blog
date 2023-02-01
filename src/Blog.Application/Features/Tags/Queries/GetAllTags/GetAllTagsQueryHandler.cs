using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IQueryHandler<GetAllTagsQuery, GetAllTagsQueryResult>
{
    private readonly IApplicationContext _db;

    public GetAllTagsQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetAllTagsQueryResult> Handle(GetAllTagsQuery query, CancellationToken ct)
    {
        var tags = await _db.Tags
            .ProjectToType<GetAllTagsQueryResultItem>()
            .ToListAsync(ct);

        return new() { Tags = tags };
    }
}