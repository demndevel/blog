using Application.Features.Notes.Commands.CreateNote;
using Application.Features.Notes.Commands.DeleteNote;
using Application.Features.Notes.Commands.UpdateNote;
using Application.Features.Notes.Queries.GetAllNotes;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Configs;
using Web.Models;
using Web.Repository.Interfaces;
using Web.Unit_of_work;

namespace Web.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TokenConfig _config;
    private readonly IRepository<Project> _projects;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ICommandHandler<CreateNoteCommand, long> _createNoteCommand;
    private readonly ICommandHandler<DeleteNoteCommand, Unit> _deleteNoteCommand;
    private readonly ICommandHandler<UpdateNoteCommand, Unit> _updateNoteCommand;

    private readonly IQueryHandler<GetAllNotesQuery, GetAllNotesQueryResult> _getAllNotesQueryHandler;

    public AdminController(ILogger<HomeController> logger, IOptions<TokenConfig> config, IRepository<Project> projects, IUnitOfWork unitOfWork, ICommandHandler<CreateNoteCommand, long> createNoteCommand, ICommandHandler<DeleteNoteCommand, Unit> deleteNoteCommand, ICommandHandler<UpdateNoteCommand, Unit> updateNoteCommand, IQueryHandler<GetAllNotesQuery, GetAllNotesQueryResult> getAllNotesQueryHandler)
    {
        _logger = logger;
        _config = config.Value;
        _projects = projects;
        _unitOfWork = unitOfWork;
        _createNoteCommand = createNoteCommand;
        _deleteNoteCommand = deleteNoteCommand;
        _updateNoteCommand = updateNoteCommand;
        _getAllNotesQueryHandler = getAllNotesQueryHandler;
    }

    [Route("")]
    public IActionResult Admin()
    {
        return View();
    }
    
    [Route("posts")]
    public async Task<IActionResult> AdminPosts()
    {
        var command = new GetAllNotesQuery();
        var result = await _getAllNotesQueryHandler.Handle(command, CancellationToken.None);
        
        return View(model: result);
    }
    
    [Route("projects")]
    public async Task<IActionResult> AdminProjects()
    {
        ViewBag.projects = await _projects.GetArray();
        return View();
    }

    [HttpPost("createNote")] // todo: authorization
    public async Task<IActionResult> CreateNote([FromForm] CreateNoteModel model)
    {
        var command = new CreateNoteCommand
        {
            Title = model.Title,
            Text = model.Text,
            Tags = model.Tags,
            ShortDescription = model.ShortDescription
        };

        return Ok(await _createNoteCommand.Handle(command, CancellationToken.None));
    }

    [HttpPost("editNote")]
    public async Task<IActionResult> EditNote([FromForm] EditNoteModel model)
    {
        var cmd = new UpdateNoteCommand
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            Tags = model.Tags,
            ShortDescription = model.ShortDescription
        };
        
        await _updateNoteCommand.Handle(cmd, CancellationToken.None);
        
        return Ok();
    }

    [HttpPost("deleteNote")]
    public async Task<IActionResult> DeleteNote([FromForm] int id)
    {
        var cmd = new DeleteNoteCommand { Id = id };
        await _deleteNoteCommand.Handle(cmd, CancellationToken.None);
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