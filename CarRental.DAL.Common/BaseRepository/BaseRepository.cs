using CarRental.DAL.Common.Paging;
using CarRental.DAL.Common.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable enable

namespace CarRental.DAL.Common.Repositories;

public class BaseRepository<TKey, TEntity>(DbContext dbContext) : IBaseRepository<TKey, TEntity>
    where TEntity : class
{
    private readonly DbContext _dbContext = dbContext;

    public virtual async Task<TEntity?> GetByIdAsync(TKey? id, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id, ct], cancellationToken: ct);
    }
    public virtual async Task<TEntity?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([name, ct], cancellationToken: ct);
    }

    public virtual async Task<List<TEntity>?> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _dbContext.Set<TEntity>().ToListAsync(ct);
        return result;
    }

    public virtual async Task<EntitiesPagingResponse<TEntity>?> SearchWithPagingAsync(
        EntitiesPagingRequest<TEntity> request,
        CancellationToken ct = default)
    {
        var itemsToReturn = _dbContext.Set<TEntity>().AsQueryable();

        if (request.Filter is not null)
        {
            itemsToReturn = itemsToReturn.Where(request.Filter);
        }

        if (request.Sorting is not null)
        {
            itemsToReturn = request.Sorting(itemsToReturn);
        }

        var totalItemsCount = await itemsToReturn.CountAsync(ct);
        var pagedItems = await itemsToReturn
            .Skip((request.PageNumber - 1) * request.PerPage)
            .Take(request.PerPage)
            .ToListAsync(ct);

        var response = new EntitiesPagingResponse<TEntity>
        {
            Items = pagedItems,
            ItemsTotalCount = totalItemsCount,
            PageNumber = request.PageNumber,
            PerPage = request.PerPage,
        };
        return response;
    }

    public virtual async Task<TEntity?> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
        var result = _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
        var primaryKey = entityType?.FindPrimaryKey();

        var keyValues = primaryKey?.Properties
                                     .Select(p => entity.GetType().GetProperty(p.Name)?.GetValue(entity))
                                     .ToArray();

        var existingEntity = await _dbContext.FindAsync<TEntity>(keyValues);

        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(ct);
            return existingEntity;
        }

        return null;
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(TKey id, CancellationToken ct = default)
    {
        var existingEntity = await GetByIdAsync(id, ct);
        if (existingEntity is null)
        {
            return;
        }

        await DeleteAsync(existingEntity, ct);
    }
}

