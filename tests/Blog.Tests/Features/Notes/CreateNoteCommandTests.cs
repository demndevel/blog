using Application.Features.Notes.Commands.CreateNote;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class CreateNoteCommandTests
{
    [Fact]
    public async Task CreateNote_Success()
    {
        var db = TestDbContext.Create();
        const string testTitle = "test title";
        const string testText = "test text";
        const string testTags = "tag1;tag2;tag3";
        const string testDescription = "some test description";
        var cmd = new CreateNoteCommand
        {
            Title = testTitle,
            Text = testText,
            Tags = testTags,
            ShortDescription = testDescription
        };
        var handler = new CreateNoteCommandHandler(db);

        var id = await handler.Handle(cmd, CancellationToken.None);

        var note = db.Notes.FirstOrDefault(n => n.Id == id);
        Assert.Equal(testTitle, note!.Title);
        Assert.Equal(testText, note.Text);
        Assert.Equal(testDescription, note.ShortDescription);
        
        TestDbContext.Destroy(db);
    }
}