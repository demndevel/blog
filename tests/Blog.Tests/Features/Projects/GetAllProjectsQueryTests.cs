using Application.Features.Projects.Queries.GetAllProjects;
using Blog.Tests.Helpers;

namespace Blog.Tests.Features.Projects;

public class GetAllProjectsQueryTests
{
    [Fact]
    public async Task GetAllProjects_Success()
    {
        var db = TestDbContext.Create();
        var query = new GetAllProjectsQuery();
        var handler = new GetAllProjectsQueryHandler(db);

        var result = await handler.Handle(query, CancellationToken.None);

        var expectedProjectCount = db.Projects.Count();
        Assert.NotNull(result);
        Assert.NotEmpty(result.Projects);
        Assert.Equal(expectedProjectCount, result.Projects.Count);
        
        TestDbContext.Destroy(db);
    }
}