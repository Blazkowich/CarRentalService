namespace CarRental.DAL.Common.BaseUnitOfWork;

public interface IBaseUnitOfWork
{
    Task SaveAsync(CancellationToken ct = default);
}
