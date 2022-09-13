using Blog.Unit_of_work;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class TagsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public TagsController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public IActionResult Tags()
    {
        var tags = _unitOfWork.Tags.GetArray();
        
        ViewBag.tags = tags;
        
        return View();
    }
    
    public IActionResult Tag(int id)
    {
        var tag = _unitOfWork.Tags.GetById(id);
        var tags = _unitOfWork.Tags.GetArray();
        var notes = _unitOfWork.Tags.GetNotesByTag(id);
        
        ViewBag.tag = tag;
        ViewBag.tags = tags;
        ViewBag.notes = notes;
        
        return View();
    }
}