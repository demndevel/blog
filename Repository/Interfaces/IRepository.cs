using Blog.Models;

namespace Blog.Repository.Interfaces;

public interface IRepository<T>
{
    T GetById(long id);
    long GetCount();
    void Insert(T entity);
    void Update(long id, T entity);
    void Delete(T entity);
    T[] GetArray();
}