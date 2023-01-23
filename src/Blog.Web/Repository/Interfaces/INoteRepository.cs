using Domain.Entities;

namespace Web.Repository.Interfaces;

public interface INoteRepository : IRepository<Note>
{
    List<Note> GetPagedList(int page, int pageSize);
}