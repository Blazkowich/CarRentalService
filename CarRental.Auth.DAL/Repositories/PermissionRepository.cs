using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Auth.DAL.Repositories;

internal class PermissionsRepository(CarRentalAuthDbContext gameStoreAuthDbContext) :
    BaseRepository<Guid, PermissionEntity>(gameStoreAuthDbContext), IPermissionsRepository
{
    private readonly CarRentalAuthDbContext _gameStoreAuthDbContext = gameStoreAuthDbContext;

    public async Task<List<PermissionEntity>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        return await _gameStoreAuthDbContext.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}