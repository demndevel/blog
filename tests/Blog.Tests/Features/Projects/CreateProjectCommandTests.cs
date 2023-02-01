using Application.Features.Projects.Commands.CreateProject;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Projects;

public class CreateProjectCommandTests
{
    [Fact]
    public async Task CreateProject_Success()
    {
        var db = TestDbContext.Create();
        var handler = new CreateProjectCommandHandler(db);
        var cmd = new CreateProjectCommand
        {
            Title = "Test Project",
            ShortDescription = "Test Project Description",
            Link = "https://example.com"
        };
     
        var result = await handler.Handle(cmd, CancellationToken.None);

        var project = await db.Projects.FindAsync(result);
        Assert.NotNull(project);
        Assert.Equal(cmd.Title, project.Title);
        Assert.Equal(cmd.ShortDescription, project.ShortDescription);
        Assert.Equal(cmd.Link, project.Link);
    }
}