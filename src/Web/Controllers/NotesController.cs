using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Repository.Interfaces;

namespace Web.Controllers;

[Route("note")]
public class NotesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INoteRepository _notes;
    private readonly ITagRepository _tags;
    
    public NotesController(INoteRepository notes, ITagRepository tags, ILogger<HomeController> logger)
    {
        _logger = logger;

        _notes = notes;
        _tags = tags;
    }

    [Route("/blog")]
    public IActionResult Blog()
    {
        return RedirectToAction("BlogByPage", routeValues: new { page = 1 });
    }
    
    [Route("/blog/{page:int}")]
    public async Task<IActionResult> BlogByPage(int page)
    {
        if (page < 1)
            return RedirectToAction("BlogByPage", routeValues: new { page = 1 });
            
        var notesCount = await _notes.GetCount();
        var notes = _notes.GetPagedList(page, 10);
        var tags = await _tags.GetArray();
        
        if (page + 1 <= notesCount / 10 + 1)
            ViewBag.next = page + 1;
        else
            ViewBag.next = -1;
        
        if (page - 1 > 0)
            ViewBag.previous = page - 1;
        else
            ViewBag.previous = -1;
        
        return View("Blog", new BlogViewModel { Notes = notes, Tags = tags });
    }
    
    [Route("{id:int}")]
    public async Task<IActionResult> Note(int id)
    {
        var note = await _notes.GetById(id);

        ViewBag.note = note;
        ViewBag.tags = await _tags.GetArray();
        
        return View();
    }
    
    [Route("/archive")]
    public async Task<IActionResult> Archive()
    {
        ViewBag.notes = await _notes.GetArray();

        return View();
    }
}