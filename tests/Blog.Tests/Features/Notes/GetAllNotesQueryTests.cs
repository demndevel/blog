using Application.Features.Notes.Queries.GetAllNotes;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class GetAllNotesQueryTests
{
    [Fact]
    public async Task GetAllNotes_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetAllNotesQuery();
        var handler = new GetAllNotesQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);

        var noteCount = db.Notes.Count();
        Assert.NotNull(result);
        Assert.NotEmpty(result.Notes);
        Assert.Equal(noteCount, result.Notes.Count);
        
        TestDbContext.Destroy(db);
    }
}