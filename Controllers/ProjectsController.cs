using Blog.Unit_of_work;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public ProjectsController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public IActionResult Projects()
    {
        var projects = _unitOfWork.Projects.GetArray();

        ViewBag.projects = projects;
        
        return View();
    }
}