using Application.Interfaces;
using Application.Persistence;
using Domain.Entities.Comment;

namespace Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, Guid>
{
    private readonly ApplicationContext _db;

    public CreateCommentCommandHandler(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateCommentCommand command, CancellationToken ct)
    {
        var comment = CreateComment(command);
        await _db.Comments.AddAsync(comment, ct);
        await _db.SaveChangesAsync(ct);
        
        return comment.Id;
    }
    
    private Comment CreateComment(CreateCommentCommand command)
    {
        return new()
        {
            PostId = command.PostId,
            Name = new Name(command.Name).Value,
            Text = new Text(command.Text).Value,
            IsAdmin = command.IsAdmin,
            DateCreated = DateTime.Now,
            Read = false
        };
    }
}