using Application.Features.Comments.Queries.GetCommentByPostId;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents;

[ViewComponent(Name = "Comments")]
public class CommentsViewComponent : ViewComponent
{
    private readonly IQueryHandler<GetCommentByPostIdQuery, GetCommentByPostIdQueryResult> _getComments;

    public CommentsViewComponent(IQueryHandler<GetCommentByPostIdQuery, GetCommentByPostIdQueryResult> getComments)
    {
        _getComments = getComments;
    }

    public async Task<IViewComponentResult> InvokeAsync(long id)
    {
        var result = await _getComments.Handle(new GetCommentByPostIdQuery { Id = id }, CancellationToken.None);

        return View(model: new GetCommentsByNoteIdViewModel { Comments = result.Comments, PostId = id });
    }
}