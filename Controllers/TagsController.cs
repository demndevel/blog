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
    
    public async Task<IActionResult> Tags()
    {
        var tags = await _tags.GetArray();
        
        ViewBag.tags = tags;
        
        return View();
    }
    
    public async Task<IActionResult> Tag(int id)
    {
        var tag = await _tags.GetById(id);
        var tags = await _tags.GetArray();
        var notes = _tags.GetNotesByTag(tag.Text);
        
        ViewBag.tag = tag;
        ViewBag.tags = tags;
        ViewBag.notes = notes;
        
        return View();
    }
}