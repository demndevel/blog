using Application.Interfaces;
using Application.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler : IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult>
{
    private readonly ApplicationContext _db;

    public GetAllProjectsQueryHandler(ApplicationContext db)
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