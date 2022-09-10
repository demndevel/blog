using Blog.Models;
using Blog.Repository.Implementations;
using Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Controllers;

public class AdminController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Note> _notesRepository;
    private readonly IRepository<Tag> _tagsRepository;
    private readonly IRepository<Project> _projectsRepository;
    private readonly TokenConfig _config;

    public AdminController(ILogger<HomeController> logger, IOptions<TokenConfig> config,  ApplicationContext db)
    {
        _notesRepository = new NoteRepository(db);
        _tagsRepository = new TagRepository(db);
        _projectsRepository = new ProjectRepository(db);

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

        _notesRepository.Insert(note);
        _notesRepository.Save();

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
        
        _notesRepository.Update(id, newNote);
        _notesRepository.Save();

        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteNote(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _notesRepository.Delete(_notesRepository.GetById(id));
        _notesRepository.Save();
        
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
        
        _projectsRepository.Insert(project);
        _projectsRepository.Save();
                
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditProject(int id, string password, string title, string description, string link)
    {
        if (!CheckPassword(password))
            return Forbid();

        _projectsRepository.Update(id, new Project
        {
            Title = title,
            ShortDescription = description,
            Link = link
        });
        _projectsRepository.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult DeleteProject(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _projectsRepository.Delete(_projectsRepository.GetById(id));
        _projectsRepository.Save();
        
        return Ok();
    }

    [HttpPost]
    public IActionResult AddTag(string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();

        _tagsRepository.Insert(new Tag
        {
            Text = text
        });
        _tagsRepository.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult EditTag(int id, string text, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _tagsRepository.Update(id, new Tag
        {
            Text = text
        });
        _tagsRepository.Save();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult DeleteTag(int id, string password)
    {
        if (!CheckPassword(password))
            return Forbid();
        
        _tagsRepository.Delete(_tagsRepository.GetById(id));
        _tagsRepository.Save();

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