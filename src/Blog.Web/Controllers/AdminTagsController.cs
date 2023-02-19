using Application.Features.Tags.Commands.CreateTag;
using Application.Features.Tags.Commands.DeleteTag;
using Application.Features.Tags.Queries.GetAllTags;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers;

[Authorize, NotFoundExceptionFilter]
public class AdminTagsController : Controller
{
    private readonly IQueryHandler<GetAllTagsQuery,GetAllTagsQueryResult> _getAllTagsHandler;
    private readonly ICommandHandler<CreateTagCommand, int> _createTagHandler;
    private readonly ICommandHandler<DeleteTagCommand, Unit> _deleteTagHandler;

    public AdminTagsController(IQueryHandler<GetAllTagsQuery, GetAllTagsQueryResult> getAllTagsHandler, ICommandHandler<CreateTagCommand, int> createTagHandler, ICommandHandler<DeleteTagCommand, Unit> deleteTagHandler)
    {
        _getAllTagsHandler = getAllTagsHandler;
        _createTagHandler = createTagHandler;
        _deleteTagHandler = deleteTagHandler;
    }

    [Route("/admin/tags")]
    public async Task<IActionResult> AdminTags()
    {
        var query = new GetAllTagsQuery();
        var results = await _getAllTagsHandler.Handle(query, CancellationToken.None);
        
        return View(model: results);
    }
    
    [HttpPost("/admin/addTag")]
    public async Task<IActionResult> AddTag(string text)
    {
        var cmd = new CreateTagCommand { Text = text };
        var id = await _createTagHandler.Handle(cmd, CancellationToken.None);
        
        return Ok($"Created new tag (id: {id}).");
    }
    
    [HttpPost("/admin/deleteTag")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var cmd = new DeleteTagCommand { Id = id };
        await _deleteTagHandler.Handle(cmd, CancellationToken.None);
        
        return Ok("Deleted");
    }
}