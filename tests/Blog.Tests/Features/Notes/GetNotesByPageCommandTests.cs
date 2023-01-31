using Application.Features.Notes.Queries.GetNotesByPage;
using Blog.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.Features.Notes;

public class GetNotesByPageCommandTests
{
    [Fact]
    public async Task GetNotesByPage_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetNotesByPageQuery { Page = 0 };
        var handler = new GetNotesByPageQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        var count = await db.Notes.CountAsync();
        Assert.Equal(count, result.Notes.Count);
        
        TestDbContext.Destroy(db);
    }
}