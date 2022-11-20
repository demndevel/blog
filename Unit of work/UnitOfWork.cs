namespace Blog.Unit_of_work;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    private readonly IServiceProvider _services;

    public UnitOfWork(ApplicationContext context, IServiceProvider services)
    {
        _services = services;
        _context = context;
    }

    public Task Save(CancellationToken ct = default)
    {
        _context.SaveChangesAsync(ct);
        return Task.CompletedTask;
    }
}
