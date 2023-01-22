namespace Web.Unit_of_work;

public interface IUnitOfWork
{
    public Task Save(CancellationToken ct = default);
}