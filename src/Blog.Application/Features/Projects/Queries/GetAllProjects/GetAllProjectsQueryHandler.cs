using Application.Interfaces;
using Application.Interfaces.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler : IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult>
{
    private readonly IApplicationContext _db;

    public GetAllProjectsQueryHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<GetAllProjectsQueryResult> Handle(GetAllProjectsQuery query, CancellationToken ct)
    {
        var projects = await _db.Projects
            .ProjectToType<GetAllProjectsQueryResultItem>()
            .ToListAsync(ct);

        return new GetAllProjectsQueryResult { Projects = projects };
    }
}