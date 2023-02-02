using Application.Features.Notes.Commands.IncreaseViews;
using Application.Features.Notes.Queries.GetNote;
using Application.Features.Notes.Queries.GetNoteArchive;
using Application.Features.Notes.Queries.GetNotesByPage;
using Application.Features.Notes.Queries.GetNotesByTags;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[Route("note")]
public class NotesController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IQueryHandler<GetNotesByPageQuery, GetNotesByPageQueryResult> _getNotesByPage;
    private readonly IQueryHandler<GetNoteQuery, GetNoteQueryResult> _getNote;
    private readonly IQueryHandler<GetNoteArchiveQuery, GetNoteArchiveQueryResult> _getNoteArchive;
    private readonly IQueryHandler<GetNotesByTagsQuery, GetNotesByTagsQueryResult> _getNotesByTags;

    private readonly ICommandHandler<IncreaseViewsCommand, Unit> _increaseViews;

    public NotesController(ILogger<HomeController> logger, IQueryHandler<GetNoteQuery, GetNoteQueryResult> getNote, IQueryHandler<GetNoteArchiveQuery, GetNoteArchiveQueryResult> getNoteArchive, IQueryHandler<GetNotesByPageQuery, GetNotesByPageQueryResult> getNotesByPage, IQueryHandler<GetNotesByTagsQuery, GetNotesByTagsQueryResult> getNotesByTags, ICommandHandler<IncreaseViewsCommand, Unit> increaseViews)
    {
        _logger = logger;
        _getNote = getNote;
        _getNoteArchive = getNoteArchive;
        _getNotesByPage = getNotesByPage;
        _getNotesByTags = getNotesByTags;
        _increaseViews = increaseViews;
    }

    [Route("/blog")]
    public IActionResult Blog()
    {
        return RedirectToAction("BlogByPage", routeValues: new { page = 0 });
    }
    
    [Route("/blog/{page:int}")]
    public async Task<IActionResult> BlogByPage(int page)
    {
        if (page < 0)
            return RedirectToAction("BlogByPage", routeValues: new { page = 0 });
        
        var query = new GetNotesByPageQuery { Page = page };
        var result = await _getNotesByPage.Handle(query, CancellationToken.None);
        
        var noteCount = result.Notes.Count;
        
        if (page + 1 <= noteCount / 10 + 1)
            ViewBag.next = page + 1;
        else
            ViewBag.next = -1;
        
        if (page - 1 > -1)
            ViewBag.previous = page - 1;
        else
            ViewBag.previous = -1;
        
        return View("Blog", new BlogViewModel { Notes = result.Notes });
    }
    
    [Route("{id:int}")]
    public async Task<IActionResult> Note(int id)
    {
        var query = new GetNoteQuery { Id = id };
        var result = await _getNote.Handle(query, CancellationToken.None);

        var increaseViewsCommand = new IncreaseViewsCommand { Id = id };
        await _increaseViews.Handle(increaseViewsCommand, CancellationToken.None);
        
        return View(model: new NoteViewModel { Note = result });
    }
    
    [Route("/archive")]
    public async Task<IActionResult> Archive()
    {
        var query = new GetNoteArchiveQuery();
        var result = await _getNoteArchive.Handle(query, CancellationToken.None);

        return View(model: result);
    }
    
    [Route("/tag/{tag}")]
    public async Task<IActionResult> GetNotesByTag(string tag)
    {
        var query = new GetNotesByTagsQuery { IncludedTags = tag };
        var result = await _getNotesByTags.Handle(query, CancellationToken.None);
        
        return View("NotesByTag", new NotesByTagViewModel { Notes = result.Notes, Tag = tag });
    }
    
    [Route("/search")]
    public async Task<IActionResult> SearchNotesByTags([FromForm] SearchNotesByTagsModel model)
    {
        var searchQuery = new GetNotesByTagsQuery { IncludedTags = model.IncludedTags ?? "", ExcludedTags = model.ExcludedTags ?? "" };
        var result = await _getNotesByTags.Handle(searchQuery, CancellationToken.None);
        
        return View("Search", new SearchNotesByTagsViewModel
        {
            Notes = result.Notes,
            IncludedTags = (model.IncludedTags ?? "").Split(';').ToList(),
            ExcludedTags = (model.ExcludedTags ?? "").Split(';').ToList()
        });
    }
}