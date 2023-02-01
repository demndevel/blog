using Application.Interfaces.Persistence;
using Domain.Entities.Note;
using Domain.Entities.Project;
using Domain.Entities.Tag;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.Helpers;

public static class TestDbContext
{
    public const int FirstTagId = 1;
    public const int FirstNoteId = 1;
    public const string FirstNoteTitle = "first note title";
    public const string FirstNoteText = "first note text";
    public const string FirstNoteDescription = "first note description";
    public const string FirstNoteTags = "common-tag;tag-lying-in-first-note";
    public const int FirstProjectId = 1;
    
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
        db.Notes.AddRange(new Note
        {
            Id = FirstNoteId,
            Text = FirstNoteText,
            Title = FirstNoteTitle,
            Tags = FirstNoteTags,
            Date = DateTime.Now,
            ShortDescription = FirstNoteDescription
        }, new Note
        {
            Id = 2,
            Text = "SecondNoteText",
            Title = "Second note title",
            Tags = "common-tag;tag-lying-in-second-note",
            Date = DateTime.Now,
            ShortDescription = "Second note description"
        });
    }

    private static void AddProjects(ApplicationContext db)
    {
        db.Add(new Project
        {
            Id = FirstProjectId,
            Title = "FirstProjectName",
            ShortDescription = "FirstProjectDescription",
            Link = "https://FirstProjectLink.com"
        });
    }

    public static void Destroy(IApplicationContext db)
    {
        var db1 = (ApplicationContext)db;
        db1.Database.EnsureDeleted();
        db1.Dispose();
    }
}