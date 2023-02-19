using Application.Errors;
using Application.Features.Comments.Commands.MarkCommentAsRead;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Comments;

public class MarkCommentAsReadCommandTests
{
    [Fact]
    public async Task MarkCommentAsRead_Success()
    {
        var db = TestDbContext.Create();
        var cmd = new MarkCommentAsReadCommand { Id = TestDbContext.FirstCommentId };
        var handler = new MarkCommentAsReadCommandHandler(db);

        await handler.Handle(cmd, CancellationToken.None);

        var comment = db.Comments.FirstOrDefault(c => c.Id == TestDbContext.FirstCommentId);
        Assert.True(comment!.Read);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task MarkCommentAsRead_NotFoundException()
    {
        var db = TestDbContext.Create();
        var cmd = new MarkCommentAsReadCommand { Id = Guid.NewGuid() };
        var handler = new MarkCommentAsReadCommandHandler(db);

        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(cmd, CancellationToken.None);
        });
        
        TestDbContext.Destroy(db);
    }
}