using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.Repositories;

namespace CarRental.Auth.DAL.Repositories;

internal class UserRoleRepository(CarRentalAuthDbContext gameStoreAuthDbContext) :
    BaseRepository<Guid, UserRolesEntity>(gameStoreAuthDbContext), IUserRoleRepository
{
    private readonly CarRentalAuthDbContext _gameStoreAuthDbContext = gameStoreAuthDbContext;
}


