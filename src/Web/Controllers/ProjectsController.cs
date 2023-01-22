using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Repository.Interfaces;

namespace Web.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Project> _projects;

    public ProjectsController(ILogger<HomeController> logger, IRepository<Project> projects)
    {
        _projects = projects;
        _logger = logger;
    }
    
    public async Task<IActionResult> Projects()
    {
        ProjectsViewModel vm = new() { Projects = await _projects.GetArray()};

        return View(vm);
    }
}