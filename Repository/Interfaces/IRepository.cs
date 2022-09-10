using Blog.Models;

namespace Blog.Repository.Interfaces;

public interface IRepository<T> : IDisposable
{
    T GetById(long id);
    long GetCount();
    List<T> GetPagedList(int page, int pageSize);
    void Insert(T entity);
    void Update(long id, T entity);
    void Delete(T entity);
    T[] GetArray();
    void Save();
}