using CarRental.DAL.Common.Repositories.Interfaces;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;

namespace CarRental.DAL.Repositories.Interfaces;

public interface IBookingRepository : IBaseRepository<Guid, BookingEntity>
{
    Task<List<BookingEntity>> GetBookingsByConditionAsync(BookingTypeDAL type, CancellationToken ct = default);

    Task<List<BookingEntity>> GetBookingsByVehicleIdAsync(Guid vehicleId, CancellationToken ct = default);
}
