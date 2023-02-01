namespace Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand
{
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Link { get; set; } = null!;
}