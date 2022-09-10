using Blog.Models;
using Blog.Repository.Implementations;
using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class NotesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Note> _notesRepository;
    private readonly IRepository<Tag> _tagsRepository;

    public NotesController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _notesRepository = new NoteRepository(context);
        _tagsRepository = new TagRepository(context);
        _logger = logger;
    }
    
    public IActionResult BlogByPage(int page)
    {
        if (page < 1)
            return View("Blog");
            
        var notesCount = _notesRepository.GetCount();
        var notes = _notesRepository.GetPagedList(page, 10);
        var tags = _tagsRepository.GetArray();

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
    
    public IActionResult Note(int id)
    {
        var note = _notesRepository.GetById(id);
        
        if (note is null)
            return NotFound();
        
        ViewBag.note = note;
        ViewBag.tags =_tagsRepository.GetArray();
        
        return View();
    }
}