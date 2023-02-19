using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.GetUnreadComments;

public class GetUnreadCommentsQueryHandler : IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult>
{
    private readonly IApplicationContext _db;

    public GetUnreadCommentsQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetUnreadCommentsResult> Handle(GetUnreadCommentsQuery query, CancellationToken ct)
    {
        var comments = await _db.Comments
            .Where(c => c.Read == false)
            .ProjectToType<GetUnreadCommentsResultItem>()
            .ToListAsync(ct);

        return new GetUnreadCommentsResult { Comments = comments };
    }
}