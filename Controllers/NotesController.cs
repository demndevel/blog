using Blog.Unit_of_work;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class NotesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public NotesController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult BlogByPage(int page)
    {
        if (page < 1)
            return View("Blog");
            
        var notesCount = _unitOfWork.Notes.GetCount();
        var notes = _unitOfWork.Notes.GetPagedList(page, 10);
        var tags = _unitOfWork.Tags.GetArray();
        
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
        var note = _unitOfWork.Notes.GetById(id);

        ViewBag.note = note;
        ViewBag.tags =_unitOfWork.Tags.GetArray();
        
        return View();
    }
}