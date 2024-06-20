namespace CarRental.DAL.Common.BaseUnitOfWork;

public interface IBaseUnitOfWork : IDisposable
{
    Task SaveAsync(CancellationToken ct = default);
}
