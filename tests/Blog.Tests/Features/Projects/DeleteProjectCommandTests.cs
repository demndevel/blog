using Application.Errors;
using Application.Features.Projects.Commands.DeleteProject;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Projects;

public class DeleteProjectCommandTests
{
    [Fact]
    public async Task DeleteProject_Success()
    {
        var db = TestDbContext.Create();
        var command = new DeleteProjectCommand { Id = TestDbContext.FirstProjectId };
        var handler = new DeleteProjectCommandHandler(db);
        
        await handler.Handle(command, CancellationToken.None);
        
        var project = await db.Projects.FindAsync(command.Id);
        Assert.Null(project);
        
        TestDbContext.Destroy(db);
    }
    
    [Fact]
    public async Task DeleteProject_NotFoundException()
    {
        var db = TestDbContext.Create();
        var command = new DeleteProjectCommand { Id = 555 };
        var handler = new DeleteProjectCommandHandler(db);
        
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        
        TestDbContext.Destroy(db);
    }
}