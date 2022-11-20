using Blog.Models;
using Blog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.Implementations;

public sealed class TagRepository : ITagRepository
{
    private ApplicationContext _db;

    public TagRepository(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<Tag> GetById(long id)
    {
        return (await _db.Tags.FirstOrDefaultAsync(n => n.Id == id))!;
    }

    public async Task<long> GetCount()
    {
        return await _db.Tags.CountAsync();
    }
    
    public async Task<Tag[]> GetArray()
    {
        return await _db.Tags.ToArrayAsync();
    }

    public void Insert(Tag tag)
    {
        _db.Tags.Add(tag);
    }

    public void Update(long id, Tag newTag)
    {
        var oldTag = _db.Tags.FirstOrDefault(t => t.Id == id);
        oldTag!.Text = newTag.Text;
    }

    public void Delete(Tag tag)
    {
        _db.Tags.Remove(tag);
    }

    private bool CheckForTag(Note note, string tag)
    {
        foreach (var t in note.Tags.Split(';'))
            if (t == tag)
                return true;
        return false;
    }

    public List<Note> GetNotesByTag(string tag)
    {
        return _db.Notes.ToList().FindAll(note => CheckForTag(note, tag));
    }
}