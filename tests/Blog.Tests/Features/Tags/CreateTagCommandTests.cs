using Application.Features.Tags.Commands.CreateTag;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Tags;

public class CreateTagCommandTests
{
    [Fact]
    public async Task CreateTagCommand_Success()
    {
        var db = TestDbContext.Create();
        const string testText = "some-tag";
        var cmd = new CreateTagCommand { Text = testText };
        var handler = new CreateTagCommandHandler(db);

        var id = await handler.Handle(cmd, CancellationToken.None);

        var tag = db.Tags.FirstOrDefault(t => t.Id == id);
        Assert.NotNull(tag);
        Assert.Equal(testText, tag!.Text);
        
        TestDbContext.Destroy(db);
    }
}