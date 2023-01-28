using Application.Errors;
using Application.Features.Tags.Commands.DeleteTag;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Tags;

public class DeleteTagCommandTests
{
    [Fact]
    public async Task DeleteTag_Success()
    {
        var db = TestDbContext.Create();
        var cmd = new DeleteTagCommand { Id = TestDbContext.FirstTagId };
        var handler = new DeleteTagCommandHandler(db);

        await handler.Handle(cmd, CancellationToken.None);

        var tag = db.Tags.FirstOrDefault(t => t.Id == TestDbContext.FirstTagId);
        Assert.Null(tag);
            
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task DeleteTag_NotFoundException()
    {
        var db = TestDbContext.Create();
        var cmd = new DeleteTagCommand { Id = 999 };
        var handler = new DeleteTagCommandHandler(db);
        
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(cmd, CancellationToken.None);
        });
        
        TestDbContext.Destroy(db);
    }
}