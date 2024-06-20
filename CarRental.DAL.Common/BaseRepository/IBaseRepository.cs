using CarRental.DAL.Common.Paging;

namespace CarRental.DAL.Common.Repositories.Interfaces;
#nullable enable
public interface IBaseRepository<TKey, TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);

    Task<TEntity?> GetByNameAsync(string name, CancellationToken ct = default);

    Task<List<TEntity>?> GetAllAsync(CancellationToken ct = default);

    Task<EntitiesPagingResponse<TEntity>?> SearchWithPagingAsync(EntitiesPagingRequest<TEntity> request, CancellationToken ct = default);

    Task<TEntity?> AddAsync(TEntity entity, CancellationToken ct = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken ct = default);

    Task DeleteAsync(TEntity entity, CancellationToken ct = default);

    Task DeleteAsync(TKey id, CancellationToken ct = default);
}

