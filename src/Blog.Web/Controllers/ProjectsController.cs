using Application.Features.Projects.Queries.GetAllProjects;
using Application.Interfaces;
using Domain.Entities.Project;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Repository.Interfaces;

namespace Web.Controllers;

[Route("projects")]
public class ProjectsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult> _getAllProjects;

    public ProjectsController(ILogger<HomeController> logger, IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult> getAllProjects)
    {
        _logger = logger;
        _getAllProjects = getAllProjects;
    }
    
    [Route("")]
    public async Task<IActionResult> Projects()
    {
        var query = new GetAllProjectsQuery();
        var projects = await _getAllProjects.Handle(query, CancellationToken.None);
        
        return View(model: projects);
    }
}