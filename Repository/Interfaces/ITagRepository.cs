using Blog.Models;

namespace Blog.Repository.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    List<Note> GetNotesByTag(string tag);
}