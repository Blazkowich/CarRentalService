using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.BaseRepository;

namespace CarRental.Auth.DAL.Repositories;

internal class UserRoleRepository(CarRentalAuthDbContext carRentalAuthDbContext) :
    BaseRepository<Guid, UserRolesEntity>(carRentalAuthDbContext), IUserRoleRepository
{
    private readonly CarRentalAuthDbContext _carRentalAuthDbContext = carRentalAuthDbContext;
}


