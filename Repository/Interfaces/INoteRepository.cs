using Blog.Models;

namespace Blog.Repository.Interfaces;

public interface INoteRepository : IRepository<Note>
{
    List<Note> GetPagedList(int page, int pageSize);
}