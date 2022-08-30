using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Controllers;

public class AdminController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    private readonly TokenConfig _config;

    public AdminController(ILogger<HomeController> logger, IOptions<TokenConfig> config,  ApplicationContext db)
    {
        _db = db;
        _logger = logger;
        _config = config.Value;
    }

    [HttpPost]
    public IActionResult CreateNote(string password, string title, string text, string tags, string shortDescription)
    {
        if (!CheckPassword(password))
            return Forbid();

        var note = new Note
        {
            Title = title,
            Text = text,
            ShortDescription = shortDescription,
            Tags = ParseTags(tags),
            Date = DateTime.UtcNow
        };

        _db.Add(note);
        _db.SaveChanges();

        return Ok();
    }

    [HttpPost]
    public IActionResult EditNote(int id, string shortDescription, string title, string text, string tags, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        var toEdit = _db.Notes.FirstOrDefault(n => n.Id == id);

        if (toEdit is null)
            return NotFound();

        toEdit.Title = title;
        toEdit.Text = text;
        toEdit.ShortDescription = shortDescription;
        toEdit.Tags = ParseTags(tags);

        _db.SaveChanges();
        
        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteNote(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        var toDelete = _db.Notes.FirstOrDefault(n => n.Id == id);
        
        if (toDelete is null)
            return NotFound();
        
        _db.Remove(toDelete);
        _db.SaveChanges();
        
        return Ok();
    }

    [HttpPost]
    public IActionResult AddProject(string password, string title, string description, string link)
    {
        if (!CheckPassword(password))
            return Forbid();

        var project = new Project
        {
            Title = title,
            ShortDescription = description,
            Link = link
        };

        _db.Projects.Add(project);
        _db.SaveChanges();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditProject(int id, string password, string title, string description, string link)
    {
        if (!CheckPassword(password))
            return Forbid();

        var toEdit = _db.Projects.FirstOrDefault(p => p.Id == id);

        if (toEdit is null)
            return NotFound();

        toEdit.Title = title;
        toEdit.ShortDescription = description;
        toEdit.Link = link;

        _db.SaveChanges();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult DeleteProject(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        var toDelete = _db.Projects.FirstOrDefault(p => p.Id == id);
        
        if (toDelete is null)
            return NotFound();
        
        _db.Remove(toDelete);
        _db.SaveChanges();
        
        return Ok();
    }

    [HttpPost]
    public IActionResult AddTag(string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        var tag = new Tag { Text = text };

        _db.Tags.Add(tag);

        _db.SaveChanges();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditTag(int id, string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        var toEdit = _db.Tags.FirstOrDefault(t => t.Id == id);

        if (toEdit is null)
            return NotFound();
        
        toEdit.Text = text;
        
        _db.SaveChanges();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult DeleteTag(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        var toDelete = _db.Tags.FirstOrDefault(t => t.Id == id);

        if (toDelete is null)
            return NotFound();

        _db.Tags.Remove(toDelete);
        _db.SaveChanges();
        
        return Ok();
    }
    
    private List<int> ParseTags(string tags)
    {
        List<int> parsedTags = new List<int>();

        foreach (var tag in tags.Split(';'))
        {
            parsedTags.Add(Convert.ToInt32(tag));            
        }

        return parsedTags;
    }

    private bool CheckPassword(string password)
    {
        if (password == _config.Token)
            return true;
        return false;
    }
}