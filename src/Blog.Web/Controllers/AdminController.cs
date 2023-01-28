using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Configs;
using Web.Repository.Interfaces;
using Web.Unit_of_work;

namespace Web.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TokenConfig _config;
    private readonly INoteRepository _notes;
    private readonly IRepository<Project> _projects;
    private readonly IUnitOfWork _unitOfWork;
    
    public AdminController(ILogger<HomeController> logger, IOptions<TokenConfig> config, INoteRepository notes, IRepository<Project> projects, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _config = config.Value;
        _notes = notes;
        _projects = projects;
        _unitOfWork = unitOfWork;
    }

    [Route("")]
    public IActionResult Admin()
    {
        return View();
    }
    
    [Route("posts")]
    public async Task<IActionResult> AdminPosts()
    {
        ViewBag.notes = await _notes.GetArray();
        return View();
    }
    
    [Route("projects")]
    public async Task<IActionResult> AdminProjects()
    {
        ViewBag.projects = await _projects.GetArray();
        return View();
    }

    [HttpPost("createNote")]
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

    [HttpPost("editNote")]
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

    [HttpPost("deleteNote")]
    public async Task<IActionResult> DeleteNote(int id, string password)
    {
        if (!CheckPassword(password))
            return BadRequest();

        _notes.Delete(await _notes.GetById(id));
        await _unitOfWork.Save();

        return Ok();
    }

    [HttpPost("addProject")]
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
    
    [HttpPost("editProject")]
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
    
    [HttpPost("deleteProject")]
    public async Task<IActionResult> DeleteProject(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _projects.Delete(await _projects.GetById(id));
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