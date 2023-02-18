using Application.Features.Comments.Commands.DeleteComment;
using Application.Features.Comments.Commands.MarkCommentAsRead;
using Application.Features.Comments.Queries.GetUnreadComments;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.Models;

namespace Web.Controllers;

[Authorize, NotFoundExceptionFilter]
public class AdminCommentsController : Controller
{
    private readonly IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult> _getUnreadComments;
    private readonly ICommandHandler<MarkCommentAsReadCommand, Unit> _markCommentAsRead;
    private readonly ICommandHandler<DeleteCommentCommand, Unit> _deleteComment;

    public AdminCommentsController(IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult> getUnreadComments, ICommandHandler<DeleteCommentCommand, Unit> deleteComment, ICommandHandler<MarkCommentAsReadCommand, Unit> markCommentAsRead)
    {
        _getUnreadComments = getUnreadComments;
        _deleteComment = deleteComment;
        _markCommentAsRead = markCommentAsRead;
    }

    [Route("/admin/comments")]
    public async Task<IActionResult> AdminComments()
    {
        var comments = await _getUnreadComments.Handle(new GetUnreadCommentsQuery(), CancellationToken.None);
        return View(model: new AdminCommentsViewModel { Comments = comments.Comments });
    }

    [Route("/admin/comments/markAsRead/{id:guid}")]
    public async Task<IActionResult> MarkCommentAsRead(Guid id)
    {
        var cmd = new MarkCommentAsReadCommand { Id = id };
        await _markCommentAsRead.Handle(cmd, CancellationToken.None);
        return Ok("Read");
    }
    
    [Route("/admin/comments/delete/{id:guid}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var cmd = new DeleteCommentCommand { Id = id };
        await _deleteComment.Handle(cmd, CancellationToken.None);
        return Ok("Deleted comment successfully");
    }
}