using Application.Interfaces;
using Application.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IQueryHandler<GetAllTagsQuery, GetAllTagsQueryResult>
{
    private readonly ApplicationContext _db;

    public GetAllTagsQueryHandler(ApplicationContext db)
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