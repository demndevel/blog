using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Persistence;
using Domain.Entities.Comment;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Commands.MarkCommentAsRead;

public class MarkCommentAsReadCommandHandler : ICommandHandler<MarkCommentAsReadCommand, Unit>
{
    private readonly ApplicationContext _db;

    public MarkCommentAsReadCommandHandler(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(MarkCommentAsReadCommand command, CancellationToken ct)
    {
        var comment = await _db.Comments.FirstOrDefaultAsync(c => c.Id == command.Id, ct);

        if (comment is null)
            throw new NotFoundException(nameof(Comment), command.Id.ToString());

        comment.Read = true;
        await _db.SaveChangesAsync(ct);

        return Unit.Value;
    }
}