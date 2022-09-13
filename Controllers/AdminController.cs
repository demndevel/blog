using Blog.Configs;
using Blog.Models;
using Blog.Unit_of_work;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Controllers;

public class AdminController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly TokenConfig _config;
    private readonly IUnitOfWork _unitOfWork;

    public AdminController(ILogger<HomeController> logger, IOptions<TokenConfig> config, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _config = config.Value;
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
            Tags = ParseTags(tags),
            Date = DateTime.UtcNow
        };

        _unitOfWork.Notes.Insert(note);
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
            Tags = ParseTags(tags)
        };
        
        _unitOfWork.Notes.Update(id, newNote);
        _unitOfWork.Save();        

        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteNote(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _unitOfWork.Notes.Delete(_unitOfWork.Notes.GetById(id));
        _unitOfWork.Save();

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
        
        _unitOfWork.Projects.Insert(project);
        _unitOfWork.Save();
                
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditProject(int id, string password, string title, string description, string link)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _unitOfWork.Projects.Update(id, new Project
        {
            Title = title,
            ShortDescription = description,
            Link = link
        });
        _unitOfWork.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult DeleteProject(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _unitOfWork.Projects.Delete(_unitOfWork.Projects.GetById(id));
        _unitOfWork.Save();
        
        return Ok();
    }

    [HttpPost]
    public IActionResult AddTag(string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _unitOfWork.Tags.Insert(new Tag
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
        
        _unitOfWork.Tags.Update(id, new Tag
        {
            Text = text
        });
        _unitOfWork.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult DeleteTag(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _unitOfWork.Tags.Delete(_unitOfWork.Tags.GetById(id));
        _unitOfWork.Save();

        return Ok();
    }
    
    private List<int> ParseTags(string tags)
    {
        List<int> parsedTags = new List<int>();

        foreach (var tag in tags.Split(';'))
        {
            parsedTags.Add(Convert.ToInt32(tag));            
        }

        return parsedTags;
    }

    private bool CheckPassword(string password)
    {
        if (password == _config.Token)
            return true;
        return false;
    }
}