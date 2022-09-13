using Blog.Models;
using Blog.Pagination;
using Blog.Repository.Interfaces;

namespace Blog.Repository.Implementations;

public sealed class NoteRepository : IRepository<Note>
{
    private readonly ApplicationContext _db;

    public NoteRepository(ApplicationContext db)
    {
        _db = db;
    }

    public Note GetById(long id)
    {
        return _db.Notes.FirstOrDefault(n => n.Id == id)!;
    }

    public Note[] GetArray()
    {
        return _db.Notes.ToArray();
    }
    
    public long GetCount()
    {
        return _db.Notes.Count();
    }

    public List<Note> GetPagedList(int page, int pageSize = 10)
    {
        return _db.Notes.OrderBy(n => n.Id).Reverse().GetPaged(page, pageSize).Results.ToList();
    }

    public void Insert(Note note)
    {
        _db.Notes.Add(note);
    }

    public void Update(long id, Note newNote)
    {
        var oldNote = _db.Notes.FirstOrDefault(n => n.Id == id)!;

        oldNote.Title = newNote.Title;
        oldNote.Text = newNote.Text;
        oldNote.ShortDescription = newNote.ShortDescription;
        oldNote.Tags = newNote.Tags;
    }

    public void Delete(Note note)
    {
        _db.Notes.Remove(_db.Notes.Find(note)!);
    }
}