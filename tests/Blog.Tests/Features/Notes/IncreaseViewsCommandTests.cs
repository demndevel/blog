using Application.Errors;
using Application.Features.Notes.Commands.IncreaseViews;
using Application.Helpers;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class IncreaseViewsCommandTests
{
    [Fact]
    public async Task IncreaseViews_Success()
    {
        var db = TestDbContext.Create();
        var command = new IncreaseViewsCommand { Id = TestDbContext.FirstNoteId };
        var handler = new IncreaseViewsCommandHandler(db);
        var initialViews = db.Notes.First(n => n.Id == TestDbContext.FirstNoteId).Views;

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(initialViews + 1, db.Notes.First(n => n.Id == TestDbContext.FirstNoteId).Views);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task IncreaseViews_NoteNotFound()
    {
        var db = TestDbContext.Create();
        var command = new IncreaseViewsCommand { Id = 999 };
        var handler = new IncreaseViewsCommandHandler(db);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        
        TestDbContext.Destroy(db);
    }
}