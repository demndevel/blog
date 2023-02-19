using Application.Features.Comments.Commands.CreateComment;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Web.Filters;
using Web.Models;

namespace Web.Controllers;

[Route("comments")]
public class CommentsController : Controller
{
    private readonly ICommandHandler<CreateCommentCommand, Guid> _createComment;

    public CommentsController(ICommandHandler<CreateCommentCommand, Guid> createComment)
    {
        _createComment = createComment;
    }

    [Route("create/{postId:int}"), NotFoundExceptionFilter, EnableRateLimiting("Comments")]
    public async Task<IActionResult> CreateComment(long postId, [FromForm] CreateCommentModel model)
    {
        var isAdmin = User.Identity is { IsAuthenticated: true };
        
        var cmd = new CreateCommentCommand
        {
            Name = model.Name,
            Text = model.Text,
            PostId = postId,
            IsAdmin = isAdmin
        };

        await _createComment.Handle(cmd, CancellationToken.None);
        
        return Redirect($"/note/{postId}#comments");
    }
}