using Application.Features.Projects.Commands.CreateProject;
using Application.Features.Projects.Commands.DeleteProject;
using Application.Features.Projects.Queries.GetAllProjects;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class AdminProjectsController : Controller
{
    private readonly ICommandHandler<CreateProjectCommand, long> _createProject;
    private readonly ICommandHandler<DeleteProjectCommand, Unit> _deleteProject;
    private readonly IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult> _getAllProjects;

    public AdminProjectsController(IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult> getAllProjects, ICommandHandler<CreateProjectCommand, long> createProject, ICommandHandler<DeleteProjectCommand, Unit> deleteProject)
    {
        _getAllProjects = getAllProjects;
        _createProject = createProject;
        _deleteProject = deleteProject;
    }

    [Route("/admin/projects")]
    public async Task<IActionResult> AdminProjects()
    {
        var query = new GetAllProjectsQuery();
        var result = await _getAllProjects.Handle(query, CancellationToken.None);
        
        return View(model: result);
    }

    [HttpPost("/admin/addProject")]
    public async Task<IActionResult> AddProject(string title, string description, string link)
    {
        var cmd = new CreateProjectCommand
        {
            Title = title,
            ShortDescription = description,
            Link = link
        };
        var id = await _createProject.Handle(cmd, CancellationToken.None);

        return Ok($"Created project {title} {id}");
    }
    
    [HttpPost("/admin/deleteProject")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var cmd = new DeleteProjectCommand { Id = id };
        await _deleteProject.Handle(cmd, CancellationToken.None);
        
        return Ok("Deleted.");
    }
}