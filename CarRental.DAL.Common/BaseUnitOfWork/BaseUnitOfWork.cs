using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Common.BaseUnitOfWork;

public abstract class BaseUnitOfWork(DbContext context) : IBaseUnitOfWork
{
    private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public Task SaveAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
