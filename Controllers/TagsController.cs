using Blog.Models;
using Blog.Repository.Implementations;
using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class TagsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITagRepository _tagsRepository;
    private readonly IRepository<Note> _notesRepository;

    public TagsController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _tagsRepository = new TagRepository(context);
        _notesRepository = new NoteRepository(context);
        _logger = logger;
    }
    
    public IActionResult Tags()
    {
        var tags = _tagsRepository.GetArray();
        
        ViewBag.tags = tags;
        
        return View();
    }
    
    public IActionResult Tag(int id)
    {
        var tag = _tagsRepository.GetById(id);
        var tags = _tagsRepository.GetArray();
        var notes = _tagsRepository.GetNotesByTag(id);
        
        ViewBag.tag = tag;
        ViewBag.tags = tags;
        ViewBag.notes = notes;
        
        return View();
    }
}