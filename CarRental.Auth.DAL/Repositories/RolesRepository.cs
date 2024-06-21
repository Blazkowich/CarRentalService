using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Auth.DAL.Repositories;

internal class RolesRepository(CarRentalAuthDbContext gameStoreAuthDbContext) :
    BaseRepository<Guid, RolesEntity>(gameStoreAuthDbContext), IRolesRepository
{
    private readonly CarRentalAuthDbContext _gameStoreAuthDbContext = gameStoreAuthDbContext;

    public async Task<List<RolesEntity>> GetRolesByUserIdAsync(Guid userId)
    {
        return await _gameStoreAuthDbContext.UserRoles
            .Where(rp => rp.UserId == userId)
            .Select(rp => rp.Role)
            .ToListAsync();
    }
}


