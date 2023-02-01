namespace Application.Features.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryResultItem
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Link { get; set; } = null!;
}