namespace Application.Features.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryResult
{
    public List<GetAllProjectsQueryResultItem> Projects { get; set; } = new();
}