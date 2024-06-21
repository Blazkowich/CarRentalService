using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.Repositories;

namespace CarRental.Auth.DAL.Repositories;

internal class UserRepository(CarRentalAuthDbContext gameStoreAuthDbContext) :
    BaseRepository<Guid, UserEntity>(gameStoreAuthDbContext), IUserRepository
{
    private readonly CarRentalAuthDbContext _gameStoreAuthDbContext = gameStoreAuthDbContext;
}


