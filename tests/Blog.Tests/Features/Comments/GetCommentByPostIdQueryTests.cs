using Application.Features.Comments.Queries.GetCommentByPostId;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Comments;

public class GetCommentByPostIdQueryTests
{
    [Fact]
    public async Task GetCommentByPostIdQueryHandler_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetCommentByPostIdQuery { Id = TestDbContext.FirstNoteId };
        var handler = new GetCommentByPostIdQueryHandler(db);
        var commentCount = db.Comments.Count(c => c.PostId == TestDbContext.FirstNoteId);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(commentCount, result.Comments.Count);
        
        TestDbContext.Destroy(db);
    }
}