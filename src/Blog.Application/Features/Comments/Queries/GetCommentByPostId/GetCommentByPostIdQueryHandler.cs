using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.GetCommentByPostId;

public class GetCommentByPostIdQueryHandler : IQueryHandler<GetCommentByPostIdQuery, GetCommentByPostIdQueryResult>
{
    private readonly IApplicationContext _db;

    public GetCommentByPostIdQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetCommentByPostIdQueryResult> Handle(GetCommentByPostIdQuery query, CancellationToken ct)
    {
        var comments = await _db.Comments
            .Where(c => c.PostId == query.Id)
            .ProjectToType<GetCommentByPostIdQueryResultItem>()
            .ToListAsync(ct);

        return new GetCommentByPostIdQueryResult { Comments = comments };
    }
}