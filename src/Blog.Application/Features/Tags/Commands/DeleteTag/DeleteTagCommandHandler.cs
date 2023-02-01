using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.Commands.DeleteTag;

public class DeleteTagCommandHandler : ICommandHandler<DeleteTagCommand, Unit>
{
    private readonly IApplicationContext _db;

    public DeleteTagCommandHandler(IApplicationContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(DeleteTagCommand command, CancellationToken ct)
    {
        var tag = await _db.Tags.FirstOrDefaultAsync(t => t.Id == command.Id, ct);

        if (tag is null)
            throw new NotFoundException(nameof(tag), command.Id.ToString());

        _db.Tags.Remove(tag);
        await _db.SaveChangesAsync(ct);

        return Unit.Value;
    }
}