using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Comment;

namespace Application.Features.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, Unit>
{
    private readonly IApplicationContext _db;

    public DeleteCommentCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(DeleteCommentCommand command, CancellationToken ct)
    {
        var comment = await _db.Comments.FindAsync(command.Id);
        
        if (comment is null)
            throw new NotFoundException(nameof(Comment), command.Id.ToString());
        
        _db.Comments.Remove(comment);
        await _db.SaveChangesAsync(ct);
        
        return Unit.Value;
    }
}