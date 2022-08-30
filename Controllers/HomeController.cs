using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    private readonly HomeConfig _config;

    public HomeController(ILogger<HomeController> logger, IOptions<HomeConfig> config,  ApplicationContext db)
    {
        _db = db;
        _logger = logger;
        _config = config.Value;
    }

    public IActionResult Index()
    {
        ViewBag.text = _config.Text;
        return View();
    }
    
    public IActionResult Projects()
    {
        ViewBag.projects = _db.Projects.ToList();
        return View();
    }
    
    public IActionResult BlogByPage(int page)
    {
        if (page < 1)
            return View("Blog");
            
        var notesCount = _db.Notes.Count();
        var notes = _db.Notes.OrderBy(n => n.Id).Reverse().GetPaged(page, 10).Results.ToList();
        var tags = _db.Tags.ToArray();

        ViewBag.notes = notes;
        ViewBag.tags = tags;

        if (page + 1 <= notesCount / 10 + 1)
            ViewBag.next = page + 1;
        else
            ViewBag.next = -1;
        
        if (page - 1 > 0)
            ViewBag.previous = page - 1;
        else
            ViewBag.previous = -1;
        
        return View("Blog");
    }
    
    public IActionResult Tags()
    {
        var tags = _db.Tags.ToArray();
        ViewBag.tags = tags;
        
        return View();
    }
    
    public IActionResult Tag(int id)
    {
        var tag = _db.Tags.FirstOrDefault(t => t.Id == id);
        var tags = _db.Tags.ToList();
        var notes = _db.Notes.ToList().FindAll(note => SearchByTag.Search(note, id));
        
        ViewBag.tag = tag!;
        ViewBag.tags = tags;
        ViewBag.notes = notes;
        
        return View();
    }

    public IActionResult Note(int id)
    {
        var note = _db.Notes.FirstOrDefault(n => n.Id == id);
        if (note is null)
            return NotFound();
        ViewBag.note = note;
        ViewBag.tags = _db.Tags.ToList();
        return View();
    }
    
    public IActionResult Admin()
    {
        /*var n = new Note { Tags = {4}, Title = "asd", ShortDescription = ",", Date = DateTime.UtcNow, Text = ""};
        var n1 = new Note { Tags = {4}, Title = "asd1", ShortDescription = ",", Date = DateTime.UtcNow, Text = ""};
        var n2 = new Note { Tags = {3,4}, Title = "asd2", ShortDescription = ",", Date = DateTime.UtcNow, Text = ""};
        var n3 = new Note { Tags = {4}, Title = "asd3", ShortDescription = ",", Date = DateTime.UtcNow, Text = ""};
        
        _db.Notes.AddRange(n, n1, n2, n3);
        _db.SaveChanges();*/
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}