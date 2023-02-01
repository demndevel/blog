using Application.Features.Tags.Queries.GetAllTags;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Tags;

public class GetAllTagsQueryTests
{
    [Fact]
    public async Task GetAllTags_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetAllTagsQuery();
        var handler = new GetAllTagsQueryHandler(db);

        var results = await handler.Handle(query, CancellationToken.None);

        var tagCount = db.Tags.Count();
        Assert.Equal(tagCount, results.Tags.Count);
        
        TestDbContext.Destroy(db);
    }
}