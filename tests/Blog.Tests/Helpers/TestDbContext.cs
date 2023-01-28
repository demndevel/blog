using Application.Interfaces.Persistence;
using Domain.Entities;
using Domain.Entities.Tag;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.Helpers;

public static class TestDbContext
{
    public const int FirstTagId = 1;
    
    public static IApplicationContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite($"Data Source={Guid.NewGuid()}.db")
            .Options;

        var context = new ApplicationContext(options);
        
        AddTags(context);
        AddNotes(context);
        AddProjects(context);
        
        context.SaveChanges();
        
        return context;
    }

    private static void AddTags(IApplicationContext db)
    {
        db.Tags.AddRange(
            new Tag
            {
                Id = FirstTagId,
                Text = "FirstTagText"
            },
            new Tag
            {
                Id = 2,
                Text = "SecondTagText"
            }
        );
    }

    private static void AddNotes(ApplicationContext db)
    {
        
    }

    private static void AddProjects(ApplicationContext db)
    {
        
    }

    public static void Destroy(IApplicationContext db)
    {
        var db1 = (ApplicationContext)db;
        db1.Database.EnsureDeleted();
        db1.Dispose();
    }
}