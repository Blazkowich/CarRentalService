using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Auth.DAL.Repositories;

internal class PermissionsRepository(CarRentalAuthDbContext carRentalAuthDbContext) :
    BaseRepository<Guid, PermissionEntity>(carRentalAuthDbContext), IPermissionsRepository
{
    private readonly CarRentalAuthDbContext _carRentalAuthDbContext = carRentalAuthDbContext;

    public async Task<List<PermissionEntity>> GetPermissionsByRoleIdAsync(Guid roleId, CancellationToken ct = default)
    {
        return await _carRentalAuthDbContext.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync(ct);
    }
}