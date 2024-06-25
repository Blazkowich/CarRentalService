using CarRental.DAL.Common.BaseRepository;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;

namespace CarRental.DAL.Repositories.Interfaces;

public interface IBookingRepository : IBaseRepository<Guid, BookingEntity>
{
    Task<List<BookingEntity>> GetBookingsByConditionAsync(BookingTypeDAL type, CancellationToken ct = default);

    Task<List<BookingEntity>> GetBookingsByVehicleIdAsync(Guid vehicleId, CancellationToken ct = default);

    Task<List<BookingEntity>> GetBookingsByUserIdAsync(Guid userId, CancellationToken ct = default);

    Task<List<BookingEntity>> GetBookingHistoryByUserId(Guid userId, CancellationToken ct = default);
}
