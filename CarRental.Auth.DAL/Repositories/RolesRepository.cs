using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Auth.DAL.Repositories;

internal class RolesRepository(CarRentalAuthDbContext carRentalAuthDbContext) :
    BaseRepository<Guid, RolesEntity>(carRentalAuthDbContext), IRolesRepository
{
    private readonly CarRentalAuthDbContext _carRentalAuthDbContext = carRentalAuthDbContext;

    public async Task<List<RolesEntity>> GetRolesByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _carRentalAuthDbContext.UserRoles
            .Where(rp => rp.UserId == userId)
            .Select(rp => rp.Role)
            .ToListAsync(ct);
    }
}


