using Application.Errors;
using Application.Features.Notes.Commands.UpdateNote;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class UpdateNoteCommandTests
{
    [Fact]
    public async Task UpdateNote_Success()
    {
        var db = TestDbContext.Create();
        const string newTitle = "new title";
        const string newText = "new text";
        const string newDescription = "new description";
        const string newTags = "tag2;tag3;tag228";
        var cmd = new UpdateNoteCommand
        {
            Id = TestDbContext.FirstNoteId,
            Text = newText,
            Title = newTitle,
            Tags = newTags,
            ShortDescription = newDescription
        };
        var handler = new UpdateNoteCommandHandler(db);

        await handler.Handle(cmd, CancellationToken.None);

        var note = db.Notes.FirstOrDefault(n => n.Title == newTitle);
        Assert.Equal(newTitle, note!.Title);
        Assert.Equal(newText, note.Text);
        Assert.Equal(newDescription, note.ShortDescription);
        Assert.Equal(newTags, note.Tags);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task UpdateNote_NotFoundException()
    {
        var db = TestDbContext.Create();
        var cmd = new UpdateNoteCommand { Id = 99999 };
        var handler = new UpdateNoteCommandHandler(db);

        await Assert.ThrowsAsync<NotFoundException>(async () 
            => await handler.Handle(cmd, CancellationToken.None));
        
        TestDbContext.Destroy(db);
    }
}