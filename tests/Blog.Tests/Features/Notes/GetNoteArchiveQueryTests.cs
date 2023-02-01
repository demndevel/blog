using Application.Features.Notes.Queries.GetNoteArchive;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class GetNoteArchiveQueryTests
{
    [Fact]
    public async Task GetNoteArchive_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetNoteArchiveQuery();
        var handler = new GetNoteArchiveQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        var noteCount = db.Notes.Count();
        Assert.NotNull(result);
        Assert.Equal(noteCount, result.Notes.Count);
        
        TestDbContext.Destroy(db);
    }
}