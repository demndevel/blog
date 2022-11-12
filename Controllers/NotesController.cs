using Blog.Models;
using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

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
    
    public IActionResult BlogByPage(int page)
    {
        if (page < 1)
            return View("Blog");
            
        var notesCount = _notes.GetCount();
        var notes = _notes.GetPagedList(page, 10);
        var tags = _tags.GetArray();
        
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
    
    public IActionResult Note(int id)
    {
        var note = _notes.GetById(id);

        ViewBag.note = note;
        ViewBag.tags = _tags.GetArray();
        
        return View();
    }
    
    public IActionResult Archive()
    {
        ViewBag.notes = _notes.GetArray();

        return View();
    }
}