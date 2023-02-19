using Application.Features.Comments.Commands.CreateComment;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Comments;

public class CreateCommentCommandTests
{
    [Fact]
    public async Task CreateComment_Success()
    {
        var db = TestDbContext.Create();
        const string testName = "John Doe";
        const string testText = "This is a comment.";
        var command = new CreateCommentCommand
        {
            PostId = 1,
            Name = testName,
            Text = testText,
            IsAdmin = false
        };
        var handler = new CreateCommentCommandHandler(db);
        var result = await handler.Handle(command, CancellationToken.None);
        
        var comment = await db.Comments.FindAsync(result);
        Assert.NotEqual(Guid.Empty, result);
        Assert.Equal(testName, comment!.Name);
        Assert.Equal(testText, comment.Text);
        Assert.False(comment.IsAdmin);

        TestDbContext.Destroy(db);
    }
}