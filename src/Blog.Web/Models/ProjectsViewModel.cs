using Domain.Entities.Project;

namespace Web.Models;

public class ProjectsViewModel
{
    public Project[] Projects { get; set; } = null!;
}