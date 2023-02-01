using Application.Errors;
using Application.Features.Notes.Queries.GetNote;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class GetNoteQueryTests
{
    [Fact]
    public async Task GetNote_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetNoteQuery { Id = TestDbContext.FirstNoteId };
        var handler = new GetNoteQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(TestDbContext.FirstNoteId, result.Id);
        Assert.Equal(TestDbContext.FirstNoteText, result.Text);
        Assert.Equal(TestDbContext.FirstNoteTitle, result.Title);
        Assert.Equal(TestDbContext.FirstNoteTags, result.Tags);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task GetNote_NotFoundException()
    {
        var db = TestDbContext.Create();
        var query = new GetNoteQuery { Id = 9999 };
        var handler = new GetNoteQueryHandler(db);
        
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
        
        TestDbContext.Destroy(db);
    }
}