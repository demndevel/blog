using Blog.Configs;
using Blog.Models;
using Blog.Repository.Interfaces;
using Blog.Unit_of_work;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TokenConfig _config;
    private readonly INoteRepository _notes;
    private readonly ITagRepository _tags;
    private readonly IRepository<Project> _projects;
    private readonly IUnitOfWork _unitOfWork;
    
    public AdminController(ILogger<HomeController> logger, IOptions<TokenConfig> config, INoteRepository notes, ITagRepository tags, IRepository<Project> projects, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _config = config.Value;
        _notes = notes;
        _tags = tags;
        _projects = projects;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Admin()
    {
        return View();
    }
    
    public async Task<IActionResult> AdminPosts()
    {
        ViewBag.notes = await _notes.GetArray();
        return View();
    }
    
    public async Task<IActionResult> AdminProjects()
    {
        ViewBag.projects = await _projects.GetArray();
        return View();
    }
    
    public async Task<IActionResult> AdminTags()
    {
        ViewBag.tags = await _tags.GetArray();
        return View();
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
            Tags = tags,
            Date = DateTime.UtcNow
        };

        _notes.Insert(note);
        _unitOfWork.Save();

        return Ok();
    }

    [HttpPost]
    public IActionResult EditNote(int id, string shortDescription, string title, string text, string tags, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        var newNote = new Note
        {
            Title = title,
            Text = text,
            ShortDescription = shortDescription,
            Tags = tags
        };
        
        _notes.Update(id, newNote);
        _unitOfWork.Save();        

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteNote(int id, string password)
    {
        if (!CheckPassword(password))
            return BadRequest();

        _notes.Delete(await _notes.GetById(id));
        await _unitOfWork.Save();

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
        
        _projects.Insert(project);
        _unitOfWork.Save();
                
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditProject(int id, string password, string title, string description, string link)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _projects.Update(id, new Project
        {
            Title = title,
            ShortDescription = description,
            Link = link
        });
        _unitOfWork.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteProject(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _projects.Delete(await _projects.GetById(id));
        await _unitOfWork.Save();
        
        return Ok();
    }

    [HttpPost]
    public IActionResult AddTag(string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _tags.Insert(new Tag
        {
            Text = text
        });
        _unitOfWork.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditTag(int id, string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _tags.Update(id, new Tag
        {
            Text = text
        });
        _unitOfWork.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteTag(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _tags.Delete(await _tags.GetById(id));
        await _unitOfWork.Save();

        return Ok();
    }
    
    private bool CheckPassword(string password)
    {
        if (password == _config.Token)
            return true;
        return false;
    }
}