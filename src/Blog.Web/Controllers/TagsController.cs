using Application.Features.Tags.Queries.GetAllTags;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("tags")]
public class TagsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IQueryHandler<GetAllTagsQuery, GetAllTagsQueryResult> _queryHandler;
    
    public TagsController(ILogger<HomeController> logger, IQueryHandler<GetAllTagsQuery, GetAllTagsQueryResult> queryHandler)
    {
        _logger = logger;
        _queryHandler = queryHandler;
    }
    
    [Route("")]
    public async Task<IActionResult> Tags(CancellationToken ct)
    {
        var query = new GetAllTagsQuery();
        var result = await _queryHandler.Handle(query, ct);
        
        return View(model: result);
    }
}