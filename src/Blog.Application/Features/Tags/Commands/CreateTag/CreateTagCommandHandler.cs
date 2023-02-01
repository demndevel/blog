using Application.Interfaces;
using Application.Interfaces.Persistence;
using Domain.Entities.Tag;

namespace Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommandHandler : ICommandHandler<CreateTagCommand, int>
{
    private readonly IApplicationContext _db;

    public CreateTagCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<int> Handle(CreateTagCommand command, CancellationToken ct)
    {
        var tag = CreateTag(command);

        await _db.Tags.AddAsync(tag, ct);
        await _db.SaveChangesAsync(ct);
        
        return tag.Id;
    }

    private Tag CreateTag(CreateTagCommand command)
    {
        return new Tag { Text = new Text(command.Text).Value };
    }
}