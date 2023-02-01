using Application.Features.Notes.Queries.GetNotesByTags;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Notes;

public class GetNotesByTagsQueryTests
{
    [Fact]
    public async Task GetNotesByTags_IncludedTags()
    {
        var db = TestDbContext.Create();
        var query = new GetNotesByTagsQuery { IncludedTags = "tag-lying-in-second-note" };
        var handler = new GetNotesByTagsQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        var notesWithSpecificTagCount = db.Notes.Count(n => n.Tags.Contains("tag-lying-in-second-note"));
        Assert.NotEmpty(result.Notes);
        Assert.Equal(notesWithSpecificTagCount, result.Notes.Count);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task GetNotesByTags_ExcludedTags()
    {
        var db = TestDbContext.Create();
        var query = new GetNotesByTagsQuery { IncludedTags = "common-tag", ExcludedTags = "tag-lying-in-second-note" };
        var handler = new GetNotesByTagsQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotEmpty(result.Notes);
        Assert.Equal(1, result.Notes.Count);
        
        TestDbContext.Destroy(db);
    }

    [Fact]
    public async Task GetNotesByTags_Empty()
    {
        var db = TestDbContext.Create();
        var query = new GetNotesByTagsQuery();
        var handler = new GetNotesByTagsQueryHandler(db);
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.Empty(result.Notes);
        
        TestDbContext.Destroy(db);
    }
}