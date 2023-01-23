namespace Web.Repository.Interfaces;

public interface IRepository<T>
{
    Task<T> GetById(long id);
    Task<long> GetCount();
    void Insert(T entity);
    void Update(long id, T entity);
    void Delete(T entity);
    Task<T[]> GetArray();
}