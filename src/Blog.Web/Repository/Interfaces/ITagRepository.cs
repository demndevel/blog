using Domain.Entities;
using Domain.Entities.Tag;

namespace Web.Repository.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    List<Note> GetNotesByTag(string tag);
}