using CarRental.Auth.DAL.Context.Entities;
using CarRental.DAL.Common.BaseRepository;

namespace CarRental.Auth.DAL.Repositories.Interfaces;

public interface IRolesRepository : IBaseRepository<Guid, RolesEntity>
{
    Task<List<RolesEntity>> GetRolesByUserIdAsync(Guid userId, CancellationToken ct = default);
}

