using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class TagsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITagRepository _tags;
    public TagsController(ILogger<HomeController> logger, ITagRepository tags)
    {
        _tags = tags;
        _logger = logger;
    }
    
    public IActionResult Tags()
    {
        var tags = _tags.GetArray();
        
        ViewBag.tags = tags;
        
        return View();
    }
    
    public IActionResult Tag(int id)
    {
        var tag = _tags.GetById(id);
        var tags = _tags.GetArray();
        var notes = _tags.GetNotesByTag(id);
        
        ViewBag.tag = tag;
        ViewBag.tags = tags;
        ViewBag.notes = notes;
        
        return View();
    }
}