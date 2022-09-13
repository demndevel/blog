using Blog.Models;
using Blog.Repository.Interfaces;

namespace Blog.Unit_of_work;

public interface IUnitOfWork
{
    public Task Save(CancellationToken ctx = default);
}