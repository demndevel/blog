using Blog.Models;
using Microsoft.AspNetCore.Mvc;
//using Blog.Models;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;

    public HomeController(ILogger<HomeController> logger,  ApplicationContext db)
    {
        _db = db;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Projects()
    {
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }
    
    public IActionResult BlogByPage(int page)
    {
        return View("Blog");
    }
    
    public IActionResult Tags()
    {
        var tags = _db.Tags.ToList();
        ViewBag.tags = tags;
        
        return View();
    }
    
    public IActionResult Rss()
    {
        return View();
    }
    
    public IActionResult Admin()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}