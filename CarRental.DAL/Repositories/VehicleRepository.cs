using CarRental.DAL.Common.Repositories;
using CarRental.DAL.Context;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories;

internal class VehicleRepository(CarRentalDbContext context) :
    BaseRepository<Guid, VehicleEntity>(context), IVehicleRepository
{
    private readonly CarRentalDbContext _context = context;
}
