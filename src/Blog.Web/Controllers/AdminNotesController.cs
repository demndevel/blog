using Application.Features.Notes.Commands.CreateNote;
using Application.Features.Notes.Commands.DeleteNote;
using Application.Features.Notes.Commands.UpdateNote;
using Application.Features.Notes.Queries.GetAllNotes;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class AdminNotesController : Controller
{
    private readonly ICommandHandler<CreateNoteCommand, long> _createNoteCommand;
    private readonly ICommandHandler<DeleteNoteCommand, Unit> _deleteNoteCommand;
    private readonly ICommandHandler<UpdateNoteCommand, Unit> _updateNoteCommand;

    private readonly IQueryHandler<GetAllNotesQuery, GetAllNotesQueryResult> _getAllNotesQueryHandler;

    public AdminNotesController(ICommandHandler<CreateNoteCommand, long> createNoteCommand, ICommandHandler<DeleteNoteCommand, Unit> deleteNoteCommand, ICommandHandler<UpdateNoteCommand, Unit> updateNoteCommand, IQueryHandler<GetAllNotesQuery, GetAllNotesQueryResult> getAllNotesQueryHandler)
    {
        _createNoteCommand = createNoteCommand;
        _deleteNoteCommand = deleteNoteCommand;
        _updateNoteCommand = updateNoteCommand;
        _getAllNotesQueryHandler = getAllNotesQueryHandler;
    }

    [Route("/admin/posts")]
    public async Task<IActionResult> AdminPosts()
    {
        var command = new GetAllNotesQuery();
        var result = await _getAllNotesQueryHandler.Handle(command, CancellationToken.None);
        
        return View(model: result);
    }
    
    [HttpPost("/admin/createNote")] // todo: authorization
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

    [HttpPost("/admin/editNote")]
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

    [HttpPost("/admin/deleteNote")]
    public async Task<IActionResult> DeleteNote([FromForm] int id)
    {
        var cmd = new DeleteNoteCommand { Id = id };
        await _deleteNoteCommand.Handle(cmd, CancellationToken.None);
        return Ok();
    }
}