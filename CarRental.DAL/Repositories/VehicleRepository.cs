using CarRental.DAL.Common.Repositories;
using CarRental.DAL.Context;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;
using CarRental.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories;

internal class VehicleRepository(CarRentalDbContext context) :
    BaseRepository<Guid, VehicleEntity>(context), IVehicleRepository
{
    private readonly CarRentalDbContext _context = context;

    public async Task<VehicleEntity> GetVehicleByNameAsync(string name, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(x => x.Name == name, ct);
    }

    public async Task<List<VehicleEntity>> GetVehiclesByTypeAsync(VehicleTypeDAL type, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(v => v.Type == type)
            .ToListAsync(ct);
    }

    public async Task<List<VehicleEntity>> GetVehiclesByReservationTypeAsync(ReservationTypeDAL type, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(v => v.ReservationType == type)
            .ToListAsync(ct);
    }

    public async Task<List<VehicleEntity>> GetAllAvailableVehicles(CancellationToken ct = default)
    {
        var bookedVehicleIds = await _context.Bookings
            .Where(b => b.EndDate >= DateTime.UtcNow && b.BookingCondition != BookingTypeDAL.Active)
            .Select(b => b.VehicleId)
            .Distinct()
            .ToListAsync(ct);

        return await _context.Vehicles
            .Where(v => !bookedVehicleIds.Contains(v.Id) && v.ReservationType != ReservationTypeDAL.Reserved)
            .ToListAsync(ct);
    }
}
