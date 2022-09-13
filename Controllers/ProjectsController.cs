using Blog.Models;
using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Project> _projects;

    public ProjectsController(ILogger<HomeController> logger, IRepository<Project> projects)
    {
        _projects = projects;
        _logger = logger;
    }
    
    public IActionResult Projects()
    {
        var projects = _projects.GetArray();

        ViewBag.projects = projects;
        
        return View();
    }
}