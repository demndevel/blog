using Application.Features.Comments.Queries.GetUnreadComments;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Comments;

public class GetUnreadCommentsQueryTests
{
    [Fact]
    public async Task GetUnreadComments_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetUnreadCommentsQuery();
        var handler = new GetUnreadCommentsQueryHandler(db);

        var result = await handler.Handle(query, CancellationToken.None);

        var unreadCommentsCount = db.Comments.Count(c => c.Read == false);
        Assert.Equal(unreadCommentsCount, result.Comments.Count);
    }
}