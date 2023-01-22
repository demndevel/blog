using Domain.Entities;

namespace Web.Repository.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    List<Note> GetNotesByTag(string tag);
}