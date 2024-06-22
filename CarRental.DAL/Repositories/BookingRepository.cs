using CarRental.DAL.Common.Repositories;
using CarRental.DAL.Context;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;
using CarRental.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories;

internal class BookingRepository(CarRentalDbContext context) :
    BaseRepository<Guid, BookingEntity>(context), IBookingRepository
{
    private readonly CarRentalDbContext _context = context;

    public async Task<List<BookingEntity>> GetBookingsByConditionAsync(BookingTypeDAL type, CancellationToken ct = default)
    {
        return await _context.Bookings
           .Where(v => v.BookingCondition == type)
           .ToListAsync(ct);
    }
}
