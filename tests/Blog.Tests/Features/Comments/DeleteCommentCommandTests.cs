using Application.Errors;
using Application.Features.Comments.Commands.DeleteComment;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Comments;

public class DeleteCommentCommandTests
{
    [Fact]
    public async Task DeleteComment_Success()
    {
        var db = TestDbContext.Create();
        var cmd = new DeleteCommentCommand { Id = TestDbContext.FirstCommentId };
        var handler = new DeleteCommentCommandHandler(db);
        
        await handler.Handle(cmd, CancellationToken.None);
        
        var comment = await db.Comments.FindAsync(TestDbContext.FirstCommentId);
        Assert.Null(comment);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task DeleteComment_NotFoundException()
    {
        var db = TestDbContext.Create();
        var cmd = new DeleteCommentCommand { Id = Guid.NewGuid() };
        var handler = new DeleteCommentCommandHandler(db);
        
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(cmd, CancellationToken.None));
        
        TestDbContext.Destroy(db);
    }
}