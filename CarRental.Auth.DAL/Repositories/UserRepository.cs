using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Auth.DAL.Repositories;

internal class UserRepository(CarRentalAuthDbContext carRentalAuthDbContext) :
    BaseRepository<Guid, UserEntity>(carRentalAuthDbContext), IUserRepository
{
    private readonly CarRentalAuthDbContext _carRentalAuthDbContext = carRentalAuthDbContext;

    public async Task<UserEntity> GetUserByUserNameAsync(string username, CancellationToken ct = default)
    {
        return await _carRentalAuthDbContext.Users
            .Where(x => x.Name == username)
            .FirstOrDefaultAsync(ct);
    }
}


