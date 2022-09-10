using Blog.Models;
using Blog.Repository.Implementations;
using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Project> _projectsRepository;

    public ProjectsController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _projectsRepository = new ProjectRepository(context);
        _logger = logger;
    }
    
    public IActionResult Projects()
    {
        var projects = _projectsRepository.GetArray();

        ViewBag.projects = projects;
        
        return View();
    }
}