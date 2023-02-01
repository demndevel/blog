using Application.Errors;
using Application.Features.Notes.Commands.DeleteNote;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class DeleteNoteCommandTests
{
    [Fact]
    public async Task DeleteNote_Success()
    {
        var db = TestDbContext.Create();
        var command = new DeleteNoteCommand { Id = TestDbContext.FirstNoteId };
        var handler = new DeleteNoteCommandHandler(db);

        await handler.Handle(command, CancellationToken.None);

        var note = db.Notes.FirstOrDefault(n => n.Id == TestDbContext.FirstNoteId);
        Assert.Null(note);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task DeleteNote_NotFoundException()
    {
        var db = TestDbContext.Create();
        var command = new DeleteNoteCommand { Id = 9999 };
        var handler = new DeleteNoteCommandHandler(db);

        await Assert.ThrowsAsync<NotFoundException>(async () 
            => await handler.Handle(command, CancellationToken.None));
        
        TestDbContext.Destroy(db);
    }
}