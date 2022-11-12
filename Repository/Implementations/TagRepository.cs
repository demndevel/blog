using Blog.Models;
using Blog.Repository.Interfaces;

namespace Blog.Repository.Implementations;

public sealed class TagRepository : ITagRepository
{
    private ApplicationContext _db;

    public TagRepository(ApplicationContext db)
    {
        _db = db;
    }
    
    public Tag GetById(long id)
    {
        return _db.Tags.FirstOrDefault(t => t.Id == id)!;
    }

    public long GetCount()
    {
        return _db.Tags.Count();
    }
    
    public Tag[] GetArray()
    {
        return _db.Tags.ToArray();
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