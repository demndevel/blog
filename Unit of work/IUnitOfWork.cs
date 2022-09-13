using Blog.Models;
using Blog.Repository.Interfaces;

namespace Blog.Unit_of_work;

public interface IUnitOfWork
{
    IRepository<Note> Notes { get; }
    ITagRepository Tags { get; }
    IRepository<Project> Projects { get; }
    void Save();
}