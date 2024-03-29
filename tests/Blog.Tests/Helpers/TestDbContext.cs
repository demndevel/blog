using Application.Persistence;
using Domain.Entities.Comment;
using Domain.Entities.Note;
using Domain.Entities.Project;
using Domain.Entities.Tag;
using Microsoft.EntityFrameworkCore;
using ApplicationContext = Application.Persistence.ApplicationContext;

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
    public static readonly Guid FirstCommentId = Guid.NewGuid();
    
    public static ApplicationContext Create()
    {
        var options = new DbContextOptionsBuilder<Application.Persistence.ApplicationContext>()
            .UseSqlite($"Data Source={Guid.NewGuid()}.db")
            .Options;

        var context = new Application.Persistence.ApplicationContext(options);

        context.Database.EnsureCreated();
        
        AddTags(context);
        AddNotes(context);
        AddProjects(context);
        AddComments(context);
        
        context.SaveChanges();
        
        return context;
    }

    private static void AddComments(ApplicationContext db)
    {
        db.Comments.AddRange(new Comment
        {
            Id = FirstCommentId,
            Name = "Amogus",
            Text = "FirstCommentText",
            PostId = FirstNoteId,
            DateCreated = DateTime.Now
        }, new Comment
        {
            Id = Guid.NewGuid(),
            Name = "Another Amogus",
            Text = "SecondCommentText",
            Read = false,
            DateCreated = DateTime.Now,
            PostId = FirstNoteId
        }, new Comment
        {
            Id = Guid.NewGuid(),
            Name = "Another Amogus",
            Text = "SecondCommentText",
            DateCreated = DateTime.Now,
            Read = true,
            PostId = FirstNoteId
        });
    }

    private static void AddTags(ApplicationContext db)
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

    private static void AddNotes(Application.Persistence.ApplicationContext db)
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

    private static void AddProjects(Application.Persistence.ApplicationContext db)
    {
        db.Add(new Project
        {
            Id = FirstProjectId,
            Title = "FirstProjectName",
            ShortDescription = "FirstProjectDescription",
            Link = "https://FirstProjectLink.com"
        });
    }

    public static void Destroy(ApplicationContext db)
    {
        var db1 = (Application.Persistence.ApplicationContext)db;
        db1.Database.EnsureDeleted();
        db1.Dispose();
    }
}